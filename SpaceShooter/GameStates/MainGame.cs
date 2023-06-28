using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SpaceShooter.Constants;
using SpaceShooter.Elements;
using SpaceShooter.GameStates.StateManagement;
using SpaceShooter.Managers;
using SpaceShooter.Managers.Animation;

namespace SpaceShooter.GameStates;

public sealed class MainGame : IState
{
    private readonly Random _random = new();
    private readonly int _maxEnemiesOnScreen = 10;
    private readonly List<Enemy> _enemies = new();

    private readonly Player _player;
    private readonly Sprite _background;
    private readonly TextureManager _textureManager;
    private readonly TextManager _textManager;
    private readonly AnimationManager _animationManager;
    private readonly SoundManager _soundManager;
    private readonly StateMachine _stateMachine;

    private int _playerTimeDeathCounter = 0;

    public MainGame()
    {
        _textureManager = new TextureManager();
        _animationManager = new AnimationManager();
        _soundManager = new SoundManager();
        _textManager = new TextManager("FreeMono");
        _stateMachine = StateMachine.Instance;

        _player = new Player(
            new Vector2f(WindowSettings.Width / 2 - 32, WindowSettings.Height / 2 - 32),
            _textureManager.PlayerTexture,
            _textureManager.BulletTexture);

        _background = new Sprite
        {
            Texture = _textureManager.BackgroundTexture
        };
    }

    public void Update() {

        if (_player.IsDestroyed is false)
        {
            _player.Update();
        }
        else
        {
            _playerTimeDeathCounter++;

            if (_playerTimeDeathCounter > WindowSettings.FrameLimit * 3)
            {
                _stateMachine.SetState(AppState.END, new object [] { _player.Score });
            }
        }

        UpdateEnemies();
        _animationManager.Update();
        _soundManager.Update();
        UpdateText();
    }

    public void Draw(RenderTarget window)
    {
        window.Draw(_background);

        if (_player.IsDestroyed is false)
        {
            _player.Draw(window);
        }

        for (var i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].Draw(window);
        }

        _textManager.Draw(window);
        _animationManager.Draw(window);
    }

    public void HandleInput()
    {
        if (_player.IsDestroyed)
        {
            return;
        }

        var moveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
        var moveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);
        var moveUp = Keyboard.IsKeyPressed(Keyboard.Key.W);
        var moveDown = Keyboard.IsKeyPressed(Keyboard.Key.S);

        var isMove = moveLeft || moveRight || moveUp || moveDown;

        if (isMove)
        {
            if (moveLeft)
            {
                _player.SetPosition(new Vector2f(_player.Position.X - _player.Speed, _player.Position.Y));
            }

            if (moveRight)
            {
                _player.SetPosition(new Vector2f(_player.Position.X + _player.Speed, _player.Position.Y));
            }

            if (moveUp)
            {
                _player.SetPosition(new Vector2f(_player.Position.X, _player.Position.Y - _player.Speed));
            }

            if (moveDown)
            {
                _player.SetPosition(new Vector2f(_player.Position.X, _player.Position.Y + _player.Speed));
            }
        }

        var isFire = Keyboard.IsKeyPressed(Keyboard.Key.Space);

        if (isFire)
        {
            if (_player.TryFire())
            {
                _soundManager.PlayShotSound();
            }
        }
    }

    private void UpdateEnemies()
    {
        if (_enemies.Count < 10)
        {
            for (var i = 0; i < _maxEnemiesOnScreen - _enemies.Count; i++)
            {
                var position = new Vector2f(_random.Next(64, (int)WindowSettings.Width - 64), _random.Next(-256, -64));

                _enemies.Add(new Enemy(position, _textureManager.EnemyTexture));
            }
        }

        for (var i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].Update();
            if (_enemies[i].Position.Y > WindowSettings.Height || IsCollision(_enemies[i]))
            {
                _enemies[i].MarkAsDestroyed();
            }
        }

        _enemies.RemoveAll(x => x.IsDestroyed);
    }

    private void UpdateText()
    {
        _textManager.TypeText("Score: " + _player.Score.ToString(), 25, Color.White, new Vector2f(10f, 10f));
    }

    private bool IsCollision(Enemy enemy)
    {
        if (_player.IsDestroyed)
        {
            return false;
        }

        if (_player.Sprite.GetGlobalBounds().Intersects(enemy.Sprite.GetGlobalBounds()))
        {
            _animationManager.Animations.Add(new FrameAnimation(_textureManager.ExplosionTextures, enemy.Position));
            _animationManager.Animations.Add(new FrameAnimation(_textureManager.ExplosionTextures, _player.Position));

            _player.MarkAsDestroyed();

            _soundManager.PlayExplosionSound();

            return true;
        }

        for (var i = 0; i < _player.Bullets.Count; i++)
        {
            if (enemy.Sprite.GetGlobalBounds().Intersects(_player.Bullets[i].Sprite.GetGlobalBounds()))
            {
                _animationManager.Animations.Add(new FrameAnimation(_textureManager.ExplosionTextures, enemy.Position));
                _player.Bullets.Remove(_player.Bullets[i]);
                _soundManager.PlayExplosionSound();
                _player.AddScore(10);

                return true;
            }
        }
        return false;
    }

    public void SetAdditionalParameters(params object[] additionalParameters)
    {
    }
} 
