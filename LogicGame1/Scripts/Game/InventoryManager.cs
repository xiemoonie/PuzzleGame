using Godot;
using System;

//This should handle the whole inventory
public class InventoryManager : HFlowContainer {


	private PackedScene itemScene = (PackedScene)GD.Load("res://Objects/Gui/InventoryItem.tscn");
	TextureRect unselected;
	TextureRect itemToDisplay;
	int itemsOnInventory = -1;
	bool itemGrabbed = false;
	InventoryItem itemToDrop;

	public override void _Ready() {
		//unselected = GetNode<TextureRect>("res://Images/Gui/unselected.png");
		itemToDisplay = GetNode<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
		GD.Print("I AM ALIVE ready!!!");
	}

    public void pickedItem(InventoryItem inventoryItem, Sprite texture)
    {
		itemTexture(inventoryItem, texture);
		

	}

public void itemAdded(InventoryItem inventoryItem){
		var invItem = inventoryItem;
		invItem.pictureOnScreenEvent += someMethod;
		//GD.Print("Hola :D ");
		this.AddChild(inventoryItem);
		itemsOnInventory = this.GetChildCount();
		//GD.Print("amount of items in inventory", +itemsOnInventory);
}

	public void itemTexture(InventoryItem inventoryItem, Sprite texture)
	{
		inventoryItem.setTexture(texture);
		itemAdded(inventoryItem);
	}

	public void dropItem(InventoryItem inventoryItem)
    {
		
			TextureRect unselectTexture = inventoryItem.GetNode<TextureRect>("Content/Texture/SelectedItem");
			itemToDisplay.Texture = null;
			unselectTexture.Visible = false;
			itemGrabbed = false;
		

		
	}
	public void selectedItem(InventoryItem inventoryItem)
    {
		if (itemGrabbed == false)
        {
			GD.Print("item grabbed");
			itemGrabbed = true;
			itemToDrop = inventoryItem;
			TextureRect itemSelected = inventoryItem.GetNode<TextureRect>("Content/Texture/SelectedItem");
			TextureRect itemSelectedTexture = inventoryItem.GetNode<TextureRect>("Content/Texture");
			itemSelected.Visible = true;
			itemToDisplay.Texture = itemSelectedTexture.Texture;
        }
        else
        {
			GD.Print("item dropped");
			dropItem(itemToDrop);
			itemGrabbed = false;
			selectedItem(inventoryItem);
			
        }


	}




	public void someMethod(){
   //GD.Print(":D I am a sad person who is still trying for no reason");

}




}

