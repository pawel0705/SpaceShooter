using SFML.Graphics;

namespace SpaceShooter.GameStates.StateManagement;

public interface IState
{
    void HandleInput();
    void Update();
    void Draw(RenderTarget window);
    void SetAdditionalParameters(params object[] additionalParameters);
}
