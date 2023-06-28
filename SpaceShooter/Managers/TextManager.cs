using SFML.Graphics;
using SFML.System;

namespace SpaceShooter.Managers;

public sealed class TextManager
{
    private const string _fontPath = "Assets/Fonts/";
    private readonly Font _font;
    private readonly List<Text> _texts;

    public TextManager(string fontFamilyName)
    {
        _font = new Font(_fontPath + fontFamilyName + ".ttf");
        _texts = new List<Text>();
    }

    public void TypeText(string text, uint fontSize, Color fontColor, Vector2f position)
    {
        var textContent = new Text(text, _font, fontSize)
        {
            Position = position,
            FillColor = fontColor
        };
        _texts.Add(textContent);
    }

    public void Draw(RenderTarget window)
    {
        for (var i = 0; i < _texts.Count; i++)
        {
            window.Draw(_texts[i]);
        }

        _texts.Clear();
    }
}
