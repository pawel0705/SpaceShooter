using SFML.Graphics;
using SFML.System;
using SpaceShooter.Elements.Base;

namespace SpaceShooter.Elements;

public sealed class Enemy : GameElement
{
    private const float _speed = 2f;
    public bool IsDestroyed { get; private set; }

    public Enemy(Vector2f position, Texture texture)
        : base(position, texture)
    {
    }

    public override void Update()
    {
        Position = new Vector2f(Position.X, Position.Y + _speed);
        Sprite.Position = Position;
    }

    public void MarkAsDestroyed()
    {
        IsDestroyed = true;
    }
}
