using SFML.Graphics;
using SFML.System;

namespace SpaceShooter.Elements.Base;

public abstract class GameElement
{
    public Sprite Sprite { get; private set; }
    public Vector2f Position { get; protected set; }

    public GameElement(Vector2f position, Texture texture)
    {
        Position = position;

        Sprite = new Sprite
        {
            Position = position,
            Texture = texture,
        };
    }

    public abstract void Update();
    public virtual void Draw(RenderTarget window)
    {
        window.Draw(Sprite);
    }
}
