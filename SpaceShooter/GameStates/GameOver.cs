using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SpaceShooter.Constants;
using SpaceShooter.GameStates.StateManagement;
using SpaceShooter.Managers;

namespace SpaceShooter.GameStates;

public sealed class GameOver : IState
{
    private readonly TextManager _textManager;
    private readonly StateMachine _stateMachine;

    private int _selectedOption = 0;
    private int _score = 0;

    private readonly string _restartText = "RESTART";
    private readonly string _exitText = "EXIT";
    private readonly string _scoreText = "Score: ";

    public GameOver()
    {
        _textManager = new TextManager("FreeMono");
        _stateMachine = StateMachine.Instance;
    }

    public void Draw(RenderTarget window)
    {
        _textManager.Draw(window);
    }

    public void HandleInput()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up))
        {
            _selectedOption = 0;

        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down))
        {
            _selectedOption = 1;
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Enter) || Keyboard.IsKeyPressed(Keyboard.Key.Space))
        {
            if (_selectedOption == 0)
            {
                _stateMachine.RemoveState(AppState.GAME);
                _stateMachine.AddState(AppState.GAME, new MainGame());
                _stateMachine.SetState(AppState.GAME);
            }
            else if (_selectedOption == 1)
            {
                _stateMachine.ExitApplication();
            }
        }
    }

    public void Update()
    {
        var scoreText = _scoreText + _score;

        _textManager.TypeText(scoreText, 50, Color.Green, new Vector2f(WindowSettings.Width / 2 - scoreText.Length * 16, WindowSettings.Height / 2 - 200));

        if (_selectedOption == 0)
        {
            _textManager.TypeText(_restartText, 50, Color.Yellow, new Vector2f(WindowSettings.Width / 2 - _restartText.Length * 16, WindowSettings.Height / 2 - 100));
            _textManager.TypeText(_exitText, 50, Color.White, new Vector2f(WindowSettings.Width / 2 - _exitText.Length * 16, WindowSettings.Height / 2));
        }
        else
        {
            _textManager.TypeText(_restartText, 50, Color.White, new Vector2f(WindowSettings.Width / 2 - _restartText.Length * 16, WindowSettings.Height / 2 - 100));
            _textManager.TypeText(_exitText, 50, Color.Yellow, new Vector2f(WindowSettings.Width / 2 - _exitText.Length * 16, WindowSettings.Height / 2));
        }
    }
    
    public void SetAdditionalParameters(params object[] additionalParameters)
    { 
        if (additionalParameters.Length > 0)
        {
            _score = int.Parse(additionalParameters[0].ToString()!);
        }
    }
}
