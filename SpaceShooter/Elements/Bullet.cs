using SFML.Graphics;
using SFML.System;
using SpaceShooter.Elements.Base;

namespace SpaceShooter.Elements;

public sealed class Bullet : GameElement
{
    private const float _speed = 20f;

    public Bullet(Vector2f position, Texture texture) 
        : base(position, texture)
    {
    }

    public override void Update()
    {
        Position = new Vector2f(Position.X, Position.Y - _speed);
        Sprite.Position = Position;
    }
}
