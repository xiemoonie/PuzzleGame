using Godot;
using System;

public class InventoryExpansionButton : TextureButton
{
	public override void _Ready()
	{
		
	}

	public override void _Pressed() {
		base._Pressed();
		GetParent<Inventory>().expand();
	}
}
