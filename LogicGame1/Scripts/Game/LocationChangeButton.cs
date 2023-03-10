using Godot;
using System;

public class LocationChangeButton : TextureButton {
    public string locationDestinationPath = "";
    public GameWrapper gameWrapper;

    public override void _Pressed() {
        base._Pressed();
        gameWrapper.loadLocation(locationDestinationPath);
    }
}