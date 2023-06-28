using SFML.Graphics;

namespace SpaceShooter.Managers.Animation;

public sealed class AnimationManager
{
    public List<FrameAnimation> Animations { get; private set; }

    public AnimationManager()
    {
        Animations = new List<FrameAnimation>();
    }

    public void Update()
    {
        for (var i = 0; i < Animations.Count; i++)
        {
            Animations[i].Update();
        }

        Animations.RemoveAll(x => x.ShouldDestroy);
    }

    public void Draw(RenderTarget window)
    {
        for (int i = 0; i < Animations.Count; i++)
        {
            Animations[i].Draw(window);
        }
    }
}
