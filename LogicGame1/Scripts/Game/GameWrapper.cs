using Godot;
using System;

public class GameWrapper : Node
{
    public string LocationToLoad = "";

    public LocationChangeButtonLeft leftLocationButton;
    public LocationChangeButtonRight rightLocationButton;
    public LocationChangeButtonBack backLocationButton;

    private Control sceneContainer;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sceneContainer = GetNode<Control>("SceneContainer");
        leftLocationButton = GetNode<LocationChangeButtonLeft>("GuiLayer/LeftLocationChange");
        rightLocationButton = GetNode<LocationChangeButtonRight>("GuiLayer/RightLocationChange");
        backLocationButton = GetNode<LocationChangeButtonBack>("GuiLayer/BackLocationChange");
        leftLocationButton.gameWrapper = this;
        rightLocationButton.gameWrapper = this;
        backLocationButton.gameWrapper = this;

        if (LocationToLoad != null && !LocationToLoad.Empty())
        {
            loadLocation(LocationToLoad);
        }

    }



    public void loadLocation(string locationToLoad)
    {
        GD.Print("LOCATION TO LOOOOOOOAD", locationToLoad);
        var template = ResourceLoader.Load<PackedScene>(locationToLoad);
        var instance = template.Instance<LocationHolder>();

        // clear children
        sceneContainer.removeAllChildren();

        sceneContainer.AddChild(instance);

        leftLocationButton.Visible = false;
        rightLocationButton.Visible = false;
        backLocationButton.Visible = false;

        if (instance.backLocationPath != null && !instance.backLocationPath.Empty())
        {
            backLocationButton.Visible = true;
            backLocationButton.locationDestinationPath = instance.backLocationPath;
        }
        if (instance.leftLocationPath != null && !instance.leftLocationPath.Empty())
        {
            leftLocationButton.Visible = true;
            leftLocationButton.locationDestinationPath = instance.leftLocationPath;
        }
        if (instance.rightLocationPath != null && !instance.rightLocationPath.Empty())
        {
            rightLocationButton.Visible = true;
            rightLocationButton.locationDestinationPath = instance.rightLocationPath;
        }
    }



    public void fetchLocation()
    {
        var instance = GetNode("SceneContainer").GetChild<LocationHolder>(GetNode("SceneContainer").GetChildCount() - 1);

        GD.Print("\n Fetching from:................... " + instance.Name + "...................");

        leftLocationButton.Visible = false;
        rightLocationButton.Visible = false;
        backLocationButton.Visible = false;

        if (instance.backLocationPath != null && !instance.backLocationPath.Empty())
        {
            backLocationButton.Visible = true;
            backLocationButton.locationDestinationPath = instance.backLocationPath;
        }
        if (instance.leftLocationPath != null && !instance.leftLocationPath.Empty())
        {
            leftLocationButton.Visible = true;
            leftLocationButton.locationDestinationPath = instance.leftLocationPath;
        }
        if (instance.rightLocationPath != null && !instance.rightLocationPath.Empty())
        {
            rightLocationButton.Visible = true;
            rightLocationButton.locationDestinationPath = instance.rightLocationPath;
        }
    }
}

