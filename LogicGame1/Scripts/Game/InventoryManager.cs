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
    List<InventoryItem> selectedItems;
    List<String> texturePath = new List<String>();
    List<String> nameSprite = new List<String>();
    [Export] string combinable;
    public override void _Ready()
    {
        itemToDisplay = GetNode<TextureRect>("/root/Main/Screen/GameWrapper/GuiLayer/GrabbedItem");
        selectedItems = new List<InventoryItem>();
    }
    public void setItemToDisplay(Texture textureDisplay)
    {
        itemToDisplay.Texture = textureDisplay;
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
        GD.Print("item grabbed");
        itemGrabbed = true;
        TextureRect itemSelected = inventoryItem.GetNode<TextureRect>("Content/Texture/SelectedItem");
        TextureRect itemSelectedTexture = inventoryItem.GetNode<TextureRect>("Content/Texture");
        itemSelected.Visible = true;
        StreamTexture combinableTexture = ResourceLoader.Load<StreamTexture>(combinable);
        if (inventoryItem.IsInGroup("Combinable"))
        {
            itemToDisplay.Texture = combinableTexture;
        }
        else if (itemToDisplay.Texture == combinableTexture)
        {
            itemToDisplay.Texture = combinableTexture;
        }
        else
        {
            if (!onlyOneSelected())
            {
                itemToDisplay.Texture = null;
                itemToDrop = null;
            }
            else
            {
                itemToDisplay.Texture = itemSelectedTexture.Texture;
                itemToDrop = inventoryItem;
            }
        }
        selectedItems.Add(inventoryItem);

    }
    public void selectCombinableItem(Texture textureDisplay)
    {
        var list = GetChildren();
        foreach (InventoryItem item in list)
        {
            TextureRect texture = item.GetNode<TextureRect>("Content/Texture");
            if (texture.Texture.ResourcePath == textureDisplay.ResourcePath)
            {
                GD.Print("combinable item selected Textures are the same");
                TextureRect itemSelected = item.GetNode<TextureRect>("Content/Texture/SelectedItem");
                itemSelected.Visible = true;
                itemToDrop = item;
                break;
            }
        }
    }

    public bool onlyOneSelected()
    {
        List<InventoryItem> selected = getSelectedItemsInventory();
        if (selected.Count == 1)
        {
            GD.Print("Only one item selected");
            return true;
        }
        else
        {
            GD.Print("More than 1 element selected");
            return false;
        }
    }
    public void unselectedItem(InventoryItem inventoryItem)
    {
        TextureRect itemSelected = inventoryItem.GetNode<TextureRect>("Content/Texture/SelectedItem");
        itemSelected.Visible = false;
        bool itemSet = false;
        List<InventoryItem> selected = getSelectedItemsInventory();
        if (selected.Count > 0)
        {
            var list = GetChildren();
            foreach (InventoryItem item in list)
            {
                for (int i = 0; i < selected.Count; i++)
                {
                    if (selected[i].IsInGroup("Combinable"))
                    {
                        StreamTexture combinableTexture = ResourceLoader.Load<StreamTexture>(combinable);
                        itemToDisplay.Texture = combinableTexture;
                        itemSet = true;
                        break;
                    }

                }
            }
            if (!itemSet)
            {
                foreach (InventoryItem item in list)
                {
                    if (!onlyOneSelected())
                    {
                        itemToDisplay.Texture = null;
                        itemToDrop = null;
                    }
                    else
                    {
                        if (selected[0].GetNode<TextureRect>("Content/Texture") == item.GetNode<TextureRect>("Content/Texture"))
                        {
                            TextureRect itemSelectedTexture = item.GetNode<TextureRect>("Content/Texture");
                            itemToDisplay.Texture = itemSelectedTexture.Texture;
                            itemToDrop = item;
                        }
                    }
                }
            }
        }
        else
        {
            itemToDisplay.Texture = null;
            itemToDrop = null;
        }
    }
    public void getSprites()
    {
        List<string> textureList = new List<string>();
        textureList = WorldDictionary.getInventoryObject();
        StreamTexture GameTemplate;

        for (int i = 0; i < textureList.Count; i++)
        {
            InventoryItem item = itemScene.Instance<InventoryItem>();
            GameTemplate = ResourceLoader.Load<StreamTexture>(textureList[i]);
            Sprite s = new Sprite();
            s.Texture = GameTemplate;
            pickedItem(item, s);
        }
    }
}

