using Godot;
using System;

public class ElectricBoxKnobExpander : Node2D {
    [Export] private string rotateBy1 = "1,0,0,0";
    [Export] private string rotateBy2 = "0,1,0,0";
    [Export] private string rotateBy3 = "0,0,1,0";
    [Export] private string rotateBy4 = "0,0,0,1";

    [Export] private string emptyInSlots = "0001";
    
    [Export] private Vector2 shiftVector = new Vector2(0, 0);

    public override void _Ready() {
        base._Ready();

        if (emptyInSlots == "0000") {
            buildWorking();
        } else {
            buildWithMissing();
        }
        
        GetNode<Sprite>("ElectricBlockKnob").Visible = false;
    }

    public void buildWithMissing() {
        var templateKnob = ResourceLoader.Load<PackedScene>("res://Objects/Locations/GateLaboratory/ElectricBlockKnobNonWorking.tscn");
        var templateEmpty = ResourceLoader.Load<PackedScene>("res://Objects/Locations/GateLaboratory/ElectricBlockKnobEmpty.tscn");

        Node2D knob0;
        Node2D knob1;
        Node2D knob2;
        Node2D knob3;

        knob0 = emptyInSlots[0] == '0' ? templateKnob.Instance<ElectricBoxKnobNonWorking>() : templateEmpty.Instance<Node2D>();
        knob1 = emptyInSlots[1] == '0' ? templateKnob.Instance<ElectricBoxKnobNonWorking>() : templateEmpty.Instance<Node2D>();
        knob2 = emptyInSlots[2] == '0' ? templateKnob.Instance<ElectricBoxKnobNonWorking>() : templateEmpty.Instance<Node2D>();
        knob3 = emptyInSlots[3] == '0' ? templateKnob.Instance<ElectricBoxKnobNonWorking>() : templateEmpty.Instance<Node2D>();
        
        this.AddChild(knob0);
        this.AddChild(knob1);
        this.AddChild(knob2);
        this.AddChild(knob3);

        knob0.GlobalPosition = this.GlobalPosition + shiftVector * 0.0f;
        knob1.GlobalPosition = this.GlobalPosition + shiftVector * 1.0f;
        knob2.GlobalPosition = this.GlobalPosition + shiftVector * 2.0f;
        knob3.GlobalPosition = this.GlobalPosition + shiftVector * 3.0f;
    }
    
    public void buildWorking() {
        var template = ResourceLoader.Load<PackedScene>("res://Objects/Locations/GateLaboratory/ElectricBlockKnob.tscn");

        var knob0 = template.Instance<ElectricBoxKnob>();
        var knob1 = template.Instance<ElectricBoxKnob>();
        var knob2 = template.Instance<ElectricBoxKnob>();
        var knob3 = template.Instance<ElectricBoxKnob>();

        knob0.Setup(knob0, knob1, knob2, knob3, rotateBy1);
        knob1.Setup(knob0, knob1, knob2, knob3, rotateBy2);
        knob2.Setup(knob0, knob1, knob2, knob3, rotateBy3);
        knob3.Setup(knob0, knob1, knob2, knob3, rotateBy4);

        this.AddChild(knob0);
        this.AddChild(knob1);
        this.AddChild(knob2);
        this.AddChild(knob3);

        knob0.rotateAll(true);
        knob1.rotateAll(true);
        knob2.rotateAll(true);
        knob3.rotateAll(true);
        
        knob0.GlobalPosition = this.GlobalPosition + shiftVector * 0.0f;
        knob1.GlobalPosition = this.GlobalPosition + shiftVector * 1.0f;
        knob2.GlobalPosition = this.GlobalPosition + shiftVector * 2.0f;
        knob3.GlobalPosition = this.GlobalPosition + shiftVector * 3.0f;

    }
}