using Godot;
using System;
using System.Collections.Generic;

public class Combine : TextureButton
{
    private PackedScene itemScene = (PackedScene)GD.Load("res://Objects/Gui/InventoryItem.tscn");
    public override void _Pressed()
    {
        base._Pressed();
        var inventory = GetNode<InventoryManager>("/root/Main/Screen/GameWrapper/GuiLayer/Inventory/MarginContainer/ScrollContainer/Inventory/InventoryContainer");

     
        List<InventoryItem> inventoryList = new List<InventoryItem>();
       
        List<string> findResourceOfSelected = new List<string>();
        inventoryList = inventory.getSelectedItemsInventory();

        foreach (var item in inventoryList)
        {
            TextureRect itemSelected = item.GetNode<TextureRect>("Content/Texture/SelectedItem");
            TextureRect itemSelectedTexture = item.GetNode<TextureRect>("Content/Texture");
            string i = itemSelectedTexture.Texture.ResourcePath;
            GD.Print($"hey hey did anyone say buuu {i}");
            string n = WorldDictionary.getInventoryCombinableObjectName(i);
            findResourceOfSelected.Add(n);
        }
        switch (findResourceOfSelected.Count)
        {
            case 2:
                
                bool createObject = true;
                foreach (string name in findResourceOfSelected)
                {
                    GD.Print($"text path" +name);
                    if (name != "Gum" && name != "Battery")
                    {
                        GD.Print($"hey hey hey hye hye no se puede combinar");
                        createObject = false;
                    }
                }
                if (createObject)
                {
                    InventoryItem item = itemScene.Instance<InventoryItem>();
                    inventory.eraseSelectedItem();
                    Sprite combinableItem = new Sprite();
                    StreamTexture texture = ResourceLoader.Load<StreamTexture>(WorldDictionary.getPathResourceObject("Fire"));
                    WorldDictionary.setStateObject("Fire", 1);
                    combinableItem.Texture = texture;
                    inventory.addItem(item, combinableItem);
                }
                break;
        }
        GameSaver.SaveGameInvenotry();
    }
}