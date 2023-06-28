using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SpaceShooter.Constants;
using SpaceShooter.GameStates.StateManagement;
using SpaceShooter.Managers;

namespace SpaceShooter.GameStates;

public sealed class MainMenu : IState
{
    private readonly TextManager _textManager;
    private readonly StateMachine _stateMachine;

    private int _selectedOption = 0;

    private readonly string _startText = "START";
    private readonly string _exitText = "EXIT";

    public MainMenu()
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
        if(_selectedOption == 0)
        {
            _textManager.TypeText(_startText, 50, Color.Yellow, new Vector2f(WindowSettings.Width / 2 - _startText.Length * 16, WindowSettings.Height / 2 - 100));
            _textManager.TypeText(_exitText, 50, Color.White, new Vector2f(WindowSettings.Width / 2 - _exitText.Length * 16, WindowSettings.Height / 2));
        }
        else
        {
            _textManager.TypeText(_startText, 50, Color.White, new Vector2f(WindowSettings.Width / 2 - _startText.Length * 16, WindowSettings.Height / 2 - 100));
            _textManager.TypeText(_exitText, 50, Color.Yellow, new Vector2f(WindowSettings.Width / 2 - _exitText.Length * 16, WindowSettings.Height / 2));
        }
    }

    public void SetAdditionalParameters(params object[] additionalParameters)
    {
    }
}
