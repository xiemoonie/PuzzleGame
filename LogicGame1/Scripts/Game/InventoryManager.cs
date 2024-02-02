using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

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
        itemToDisplay = GetNode<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
    }
    
    public void pickedItem(InventoryItem inventoryItem, Sprite texture)
    {
        addItem(inventoryItem, texture);
        nameSprite.Add(texture.Name);
    }
    public void addItem(InventoryItem inventoryItem, Sprite texture)
    {
        inventoryItem.setTexture(texture);
        AddChild(inventoryItem);
        itemsOnInventory = GetChildCount();
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

    public List<InventoryItem> getSelectedItemsInventory()
    {
        var list = GetChildren();
        List<InventoryItem> selectedItems = new List<InventoryItem>();

        foreach (InventoryItem item in list)
        {
            TextureRect itemSelected = item.GetNode<TextureRect>("Content/Texture/SelectedItem");
            if (itemSelected.Visible)
            {
                selectedItems.Add(item);
            }
        }
        return selectedItems;
    }
    public void eraseSelectedItem()
    {
        var list = GetChildren();
        foreach (InventoryItem item in list)
        {
            TextureRect itemSelected = item.GetNode<TextureRect>("Content/Texture/SelectedItem");
            if (itemSelected.Visible)
            {
                item.QueueFree();
            }
        }
    }


    public void selectedItem(InventoryItem inventoryItem)
    {
        if (itemToDrop != inventoryItem || itemGrabbed == false)
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
            if (itemToDrop != inventoryItem)
            {
                selectedItem(inventoryItem);
            }
        }
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
            pickedItem(item, s);
        }
    }
}

