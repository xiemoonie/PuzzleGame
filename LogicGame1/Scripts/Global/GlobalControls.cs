using Godot;
using System;

public class GlobalControls : Node {
	public override void _Input(InputEvent eventData) {
		if (eventData is InputEventKey eventKey) {
			if (eventKey.Scancode == (ulong)KeyList.Escape) {
				GetTree().Quit();
			}
		}
	}
}
