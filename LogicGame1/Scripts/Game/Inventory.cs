using Godot;
using System;

public class Inventory : Control {
    private const float INVENTORY_WIDTH = 300f;
    
    private bool isExpanded = false;

    private float expansion = 0f;

    private Control showButton;
    private Control hideButton;

    public override void _Ready() {
        base._Ready();
        showButton = GetNode<Control>("ShowInventoryButton");
        hideButton = GetNode<Control>("HideInventoryButton");
    }

    public void expand() {
        isExpanded = true;
        showButton.Visible = false;
        hideButton.Visible = true;
    }

    public void collapse() {
        isExpanded = false;
        hideButton.Visible = false;
        showButton.Visible = true;
    }

    public override void _Process(float delta) {
        base._Process(delta);

        if (isExpanded && expansion < 1f) {
            expansion += delta * 3f;
            expansion = Mathf.Min(1f, expansion);
            updateInventoryExpansionPosition();
        }

        if (!isExpanded && expansion > 0f) {
            expansion -= delta * 3f;
            expansion = Mathf.Max(0f, expansion);
            updateInventoryExpansionPosition();
        }
    }

    private void updateInventoryExpansionPosition() {
        var vp = GetViewportRect();
        this.RectPosition = new Vector2(vp.Size.x - INVENTORY_WIDTH * expansion * expansion, 0);
    }
}
