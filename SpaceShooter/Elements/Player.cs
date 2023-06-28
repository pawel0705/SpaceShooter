using SFML.Graphics;
using SFML.System;
using SpaceShooter.Constants;
using SpaceShooter.Elements.Base;

namespace SpaceShooter.Elements;

public sealed class Player : GameElement
{
    private readonly Texture _bulletTexture;
    private int _shootDelay = 0;

    public int Score { get; private set; }
    public int Speed { get; private set; } = 4;
    public bool IsDestroyed { get; private set; }
    public List<Bullet> Bullets;

    public Player(Vector2f position, Texture playerTexture, Texture bulletTexture)
        : base(position, playerTexture)
    {
        Bullets = new List<Bullet>();
        _bulletTexture = bulletTexture;
    }

    public override void Update()
    {
        Sprite.Position = Position;

        for (var i = 0; i < Bullets.Count; i++)
        {
            Bullets[i].Update();
        }

        Bullets.RemoveAll(x => x.Position.Y < 0);
    }

    public override void Draw(RenderTarget window)
    {
        window.Draw(Sprite);

        foreach (var bullet in Bullets)
        {
            bullet.Draw(window);
        }
    }

    public void AddScore(int score)
    {
        Score += score;
    }

    public void SetPosition(Vector2f position)
    {
        if (WindowSettings.Width < position.X + 64 ||
            WindowSettings.Height < position.Y + 64 ||
            position.X < 0 ||
            position.Y < 0)
        {
            return;
        }

        Position = position;
    }

    public bool TryFire()
    {
        _shootDelay++;
        if (_shootDelay >= 15)
        {
            Bullets.Add(new Bullet(new Vector2f(Position.X, Position.Y - 32), _bulletTexture));
            _shootDelay = 0;

            return true;
        }

        return false;
    }

    public void MarkAsDestroyed()
    {
        IsDestroyed = true;
    }
}
