using Godot;
using System;

public class GreenHousePotLocationHolder : LocationHolder
{
    private Sprite woodenPlank;
    Sprite knob;
    Area2D areaKnob;
    bool knobVisibility;
    Vector2 positionAreaKnob;
    Vector2 positionAreaFinished;
    Vector2 positionCompartiment;
    private Sprite secretCompartiment;
    private Sprite sextant;
    private bool locked;
    bool woodenPlankVisibility;

    public override void _Ready()
    {
        GameLoader.LoadScene();
        positionCompartiment = new Vector2(500,752);
        positionAreaKnob = new Vector2(1600, 231);
        positionAreaFinished = new Vector2(230, 230);
        areaKnob = GetNodeOrNull<Area2D>("Knob");
        woodenPlank = GetNodeOrNull<Sprite>("WoodenPlank");
        secretCompartiment = GetNodeOrNull<Sprite>("SecretCompartiment");
        sextant = secretCompartiment.GetNodeOrNull<Sprite>("Sextant");
        knob = areaKnob.GetNodeOrNull<Sprite>("Knob");
        int sextantValue = WorldDictionary.checkObjectStatuScene(sextant.Name);
        int secretCompartimentValue = WorldDictionary.checkObjectStatuScene(secretCompartiment.Name);
        int knobValue = WorldDictionary.checkObjectStatuScene(knob.Name);
        if (secretCompartimentValue != 0)
        {
            SceneManagerWooden(secretCompartiment, secretCompartimentValue, positionCompartiment);
        }
        if (knobValue != 0)
        {
            SceneManagerArea(areaKnob, knobValue, positionAreaKnob, knob);
         
        }
        if (sextantValue != 0)
        {
            GD.Print("Sextant value" + sextantValue);
            SceneManagerSextant(sextant, sextantValue);
        }

    }
    public void SceneManagerWooden(Sprite sprite, int state, Vector2 position)
    {
        switch (state)
        {
            case 1: sprite.QueueFree(); break;
            case 3: woodenPlank.Position = new Vector2(500, 700); 
                    sprite.Visible = true;
                    sprite.Position = position;
                    break;
        }
    }
    public void SceneManagerSextant(Sprite sprite, int state)
    {
        switch (state)
        {
            case 1: sprite.QueueFree(); break;
            case 3: sprite.QueueFree(); break;
            case 4: sprite.QueueFree(); break;
        }
    }
    public void SceneManagerArea(Area2D area, int state, Vector2 position, Sprite sprite)
    {
        switch (state)
        {
            case 2: 
                area.Position = position; 
                sprite.Visible = true;
                var attachableObj = (AttachableObject)area;
                attachableObj.unlockedSlide = true;
                break;

            case 3: area.Position = positionAreaFinished;
                    sprite.Visible = true;
                    
                break;
        }
    }
}

