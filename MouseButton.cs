using Godot;
using System;

public class MouseButton : Button
{
    public override void _Ready()
    {
        this.Connect("pressed",this, nameof(OnPressed));
    }

    void OnPressed()
    {
        GD.Print("Glu glu glu glu");
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
     
 }
}

