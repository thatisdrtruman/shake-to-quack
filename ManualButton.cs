using Godot;
using System;

public class ManualButton : TouchScreenButton
{
    public override void _Ready()
    {
        this.Connect("pressed",this, nameof(OnPressed));
    }

    void OnPressed()
    {
        GD.Print("Glu glu glu glu");
        var QuackSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        QuackSound.Stream = GD.Load<AudioStream>("res://sounds/quacksound" + (int)GD.RandRange(1, 6) + ".wav");
        QuackSound.Play();
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
     
 }
}
