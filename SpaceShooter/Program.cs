using SFML.Graphics;
using SFML.Window;
using SpaceShooter.Constants;
using SpaceShooter.GameStates;
using SpaceShooter.GameStates.StateManagement;

var mode = new VideoMode(WindowSettings.Width, WindowSettings.Height);
var window = new RenderWindow(mode, WindowSettings.Name, Styles.Close);

StateMachine.Initialize(window);
var stateMachine = StateMachine.Instance;

window.SetVerticalSyncEnabled(true);
window.SetFramerateLimit(WindowSettings.FrameLimit);

window.Closed += (sender, args) => {
    window.Close();
};

stateMachine.AddState(AppState.GAME, new MainGame());
stateMachine.AddState(AppState.MENU, new MainMenu());
stateMachine.AddState(AppState.END, new GameOver());

stateMachine.SetState(AppState.MENU);

while(window.IsOpen)
{
    window.DispatchEvents();

    stateMachine.HandleInput();
    stateMachine.Update();

    window.Clear();
    stateMachine.Draw(window);
    window.Display();
}
