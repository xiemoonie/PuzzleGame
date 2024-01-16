using Godot;
using System;
using System.Collections.Generic;

public class InventoryManager : HFlowContainer
{
    private PackedScene itemScene = (PackedScene)GD.Load("res://Objects/Gui/InventoryItem.tscn");
    TextureRect unselected;
    TextureRect itemToDisplay;
    int itemsOnInventory = -1;
    bool itemGrabbed = false;
    List<Sprite> sprite;
    InventoryItem itemToDrop;
    List<String> texturePath = new List<String>();
    List<String> nameSprite = new List<String>();

    public override void _Ready()
    {

        //unselected = GetNode<TextureRect>("res://Images/Gui/unselected.png");
        itemToDisplay = GetNode<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
        GD.Print("I AM ALIVE ready!!!");
    }
    public void pickedItem(InventoryItem inventoryItem, Sprite texture)
    {
        itemTexture(inventoryItem, texture);
        nameSprite.Add(texture.Name);

    }

    public void itemAdded(InventoryItem inventoryItem)
    {
        var invItem = inventoryItem;
        invItem.pictureOnScreenEvent += someMethod;
        this.AddChild(inventoryItem);
        itemsOnInventory = this.GetChildCount();
        
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

    public InventoryItem eraseItem()
    {
        if (itemToDrop != null)
        {
            dropItem(itemToDrop);
            itemToDrop.QueueFree();
        }
        return itemToDrop;
    }

    public void selectedItem(InventoryItem inventoryItem)
    {
        if (itemToDrop == inventoryItem)
        {
            GD.Print("same item clicked");
            if (itemGrabbed == false)
            {
                GD.Print("item grabbed true");
                itemGrabbed = true;
                itemToDrop = inventoryItem;
                TextureRect itemSelected = inventoryItem.GetNode<TextureRect>("Content/Texture/SelectedItem");
                TextureRect itemSelectedTexture = inventoryItem.GetNode<TextureRect>("Content/Texture");
                itemSelected.Visible = true;
                itemToDisplay.Texture = itemSelectedTexture.Texture;
            }
            else
            {
                GD.Print("item grabbed false");
                dropItem(itemToDrop);
                itemGrabbed = false;

            }

        }
        else
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
    }

    public void someMethod()
    {
        //GD.Print(":D I am a sad person who is still trying for no reason");
    }



    public void getSprites()
    {

        List<string> textureList = new List<string>();
        textureList = WorldDictionary.getInventoryObject();
        StreamTexture GameTemplate;

        for (int i = 0; i< textureList.Count; i++)
        {
            InventoryItem item = itemScene.Instance<InventoryItem>();
            GameTemplate = ResourceLoader.Load<StreamTexture>(textureList[i]);
            Sprite s = new Sprite();
            s.Texture = GameTemplate;

            //var gameWrapper = GameTemplate.Instance<Sprite>();
            pickedItem(item, s);
        }
    }
    public void eraseSprites()
    {
        texturePath.Clear();
    }

}

