using SFML.Graphics;

namespace SpaceShooter.Managers;

public sealed class TextureManager
{
    public Texture PlayerTexture { get; private set; }
    public Texture EnemyTexture { get; private set; }
    public Texture BackgroundTexture { get; private set; }
    public Texture BulletTexture { get; private set; }
    public List<Texture> ExplosionTextures { get; private set; }

    private readonly string _assetsPath = "Assets/Textures/";
    private readonly string _explosionAssetsPath = "Assets/Textures/Explosion/";

    public TextureManager()
    {
        PlayerTexture = new Texture(_assetsPath + "player.png");
        EnemyTexture = new Texture(_assetsPath + "enemy.png");
        BackgroundTexture = new Texture(_assetsPath + "background.png");
        BulletTexture = new Texture(_assetsPath + "bullet.png");

        ExplosionTextures = new List<Texture>();

        for (var i = 1; i < 9; i++)
        {
            ExplosionTextures.Add(new Texture(_explosionAssetsPath + "explosion_00" + i.ToString() + ".png"));
        }
    }
}
