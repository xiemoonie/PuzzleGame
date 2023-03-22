using Godot;
using System;

public class ItemOnInventory : HFlowContainer {



public override void _Ready() {

}

public void itemAdded(InventoryItem inventoryItem){
		var invItem = inventoryItem;
		invItem.pictureOnScreenEvent += someMethod;
		    GD.Print("Hola :D ");
}

public void someMethod(){
   GD.Print(":D I am a sad person who is still trying for no reason");

}
/*
public void PictureOnScreen(){
	   GD.Print("custom event");

}
*/
}

