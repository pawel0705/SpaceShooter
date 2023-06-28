using SFML.Audio;

namespace SpaceShooter.Managers;

public sealed class SoundManager
{
    private readonly SoundBuffer _shotSoundBuffer;
    private readonly SoundBuffer _explosionSoundBuffer;
    private readonly List<Sound> _sounds;

    private const string _assetsPath = "Assets/Sounds/";

    public SoundManager()
    {
        _shotSoundBuffer = new SoundBuffer(_assetsPath + "shot.wav");
        _explosionSoundBuffer = new SoundBuffer(_assetsPath + "explosion.wav");

        _sounds = new List<Sound>();
    }

    public void PlayShotSound()
    {
        var sound = new Sound(_shotSoundBuffer);
        sound.Play();
        _sounds.Add(sound);
    }

    public void PlayExplosionSound()
    {
        var sound = new Sound(_explosionSoundBuffer);
        sound.Play();
        _sounds.Add(sound);
    }

    public void Update()
    {
        for (var i = 0; i < _sounds.Count; i++)
        {
            if (_sounds[i].Status == SoundStatus.Stopped)
            {
                _sounds[i].Dispose();
                _sounds.RemoveAt(i);
            }
        }
    }
}
