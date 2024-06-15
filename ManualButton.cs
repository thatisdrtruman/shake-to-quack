using Godot;
using System;

public class ManualButton : TouchScreenButton
{
    private Sprite QuackerDuck;
    private ShaderMaterial DuckShaderMaterial;
    private float ShakeIntensity = 10f; // Adjust intensity as needed
    private float ShakeTime = (float)GD.RandRange(0.45, 0.7);    // Duration of the shake effect
    private float _shakeTimer = 0f;
    private Vector2 StartPosition; // The original position
    private float StartRotation; // The original rotation
    private Vector2 StartScale; // The original scale
    private float MaxBlurAmount = 1000f;
    public override void _Ready()
    {
         QuackerDuck = GetNode<Sprite>("../QuackerDuck");
         StartPosition = QuackerDuck.Position;
         StartRotation = QuackerDuck.Rotation;
         StartScale = QuackerDuck.Scale;
         DuckShaderMaterial = QuackerDuck.Material as ShaderMaterial;
        this.Connect("pressed",this, nameof(OnPressed));
    }

    void OnPressed()
    {
        var QuackSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        QuackSound.Stream = GD.Load<AudioStream>("res://sounds/quacksound" + (int)GD.RandRange(1, 6) + ".wav");
        QuackSound.Play();

        _shakeTimer = ShakeTime;
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
     if (_shakeTimer > 0)
        {
            // Calculate random offset within the intensity range
            float offsetXCoor = (float)Mathf.Cos(GD.Randf() * Mathf.Pi * 2) * ShakeIntensity;
            float offsetYCoor = (float)Mathf.Sin(GD.Randf() * Mathf.Pi * 2) * ShakeIntensity;
            float offsetRotation = (float)GD.RandRange(-0.02, 0.02) * ShakeIntensity;
            float offsetScale = (float)GD.RandRange(-0.2, 0.2);

            // Apply offset to the sprite's position, rotation and scale
            QuackerDuck.Position = StartPosition + new Vector2(offsetXCoor, offsetYCoor);
            QuackerDuck.Rotation = StartRotation + offsetRotation;
            QuackerDuck.Scale = StartScale + new Vector2(offsetScale, offsetScale);
            
            // Apply blur effect by adjusting the shader parameter
            if (DuckShaderMaterial != null)
            {
                float blurAmount = Mathf.Lerp(0.0f, MaxBlurAmount, _shakeTimer / ShakeTime);
                DuckShaderMaterial.SetShaderParam("blur_amount", blurAmount);
            }
            
            // Decrease the shake timer
            _shakeTimer -= delta;

            // If shake time is over, reset the sprite's position, rotation and scale
            if (_shakeTimer <= 0)
            {
                QuackerDuck.Position = StartPosition;
                QuackerDuck.Rotation = StartRotation;
                QuackerDuck.Scale = StartScale;

                // Reset blur effect
                if (DuckShaderMaterial != null)
                {
                    DuckShaderMaterial.SetShaderParam("blur_amount", 0.0f);
                }
            }
        }
 }
}
