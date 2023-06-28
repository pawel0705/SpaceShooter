using SFML.Graphics;
using SFML.System;

namespace SpaceShooter.Managers.Animation;

public sealed class FrameAnimation
{
    private readonly Sprite _sprite;
    private readonly List<Texture> _textures;

    private int _frameDuration = 0;
    private int _animationCounter = 0;

    public bool ShouldDestroy { get; private set; }

    public FrameAnimation(List<Texture> textures, Vector2f position)
    {
        _textures = textures;

        _sprite = new Sprite
        {
            Position = position,
            Texture = textures[0]
        };
    }

    public void Update()
    {
        _frameDuration++;

        if (_frameDuration > 5)
        {
            _frameDuration = 0;
        }
        else
        {
            return;
        }

        if (_animationCounter == _textures.Count - 1)
        {
            ShouldDestroy = true;
        }
        else
        {
            _animationCounter += 1;
            _sprite.Texture = _textures[_animationCounter];
        }
    }

    public void Draw(RenderTarget window)
    {
        window.Draw(_sprite);
    }
}
