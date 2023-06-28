using SFML.Graphics;

namespace SpaceShooter.GameStates.StateManagement;

public sealed class StateMachine
{
    private Dictionary<AppState, IState> _states;
    private IState _currentState;
    private RenderWindow _window;

    private static StateMachine? instance = null;

    private static readonly object padlock = new object();

    private StateMachine(RenderWindow window)
    {
        _window = window;
        _states = new Dictionary<AppState, IState>();
    }

    public static StateMachine Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    throw new InvalidOperationException("Singleton instance has not been initialized");
                }
                return instance;
            }
        }
    }

    public static void Initialize(RenderWindow window)
    {
        lock (padlock)
        {
            if (instance != null)
            {
                throw new InvalidOperationException("Singleton instance has already been initialized");
            }
            instance = new StateMachine(window);
        }
    }

    public void AddState(AppState appState, IState state)
    {
        _states[appState] = state;
    }

    public void RemoveState(AppState appState)
    {
        if (_states.ContainsKey(appState))
        {
            _states.Remove(appState);
        }
    }

    public void SetState(AppState appState, params object[] additionalParameters)
    {
        if (_states.ContainsKey(appState))
        {
            _currentState = _states[appState];
            _currentState.SetAdditionalParameters(additionalParameters);
        }
        else
        {
            throw new ArgumentException($"State '{appState}' does not exist.");
        }
    }

    public void HandleInput()
    {
        _currentState.HandleInput();
    }

    public void Update()
    {
        _currentState.Update();
    }

    public void Draw(RenderTarget window)
    {
        _currentState.Draw(window);
    }

    public void ExitApplication()
    {
        _window.Close();
    }
}
