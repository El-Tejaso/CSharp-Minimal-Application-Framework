﻿using RenderingEngine.Rendering.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Text;
using RenderingEngine.Datatypes;

namespace RenderingEngine.Rendering.ImmediateMode
{
    class FontAtlasTexture
    {

        FontAtlas _fontAtlas;
        Texture _fontTexture;

        public FontAtlasTexture(FontAtlas fontAtlas, Texture fontTexture)
        {
            _fontAtlas = fontAtlas;
            _fontTexture = fontTexture;
        }

        public FontAtlas FontAtlas { get { return _fontAtlas; } }
        public Texture FontTexture { get { return _fontTexture; } }
    }

    class TextDrawer : IDisposable
    {
        GeometryDrawer _geomDrawer;

        FontAtlasTexture _activeFont;
        Dictionary<string, FontAtlasTexture> _allLoadedFonts;

        public TextDrawer(GeometryDrawer geomDrawer)
        {
            _allLoadedFonts = new Dictionary<string, FontAtlasTexture>();
            _geomDrawer = geomDrawer;

            SetCurrentFont("", -1);
        }


        private static string GenerateKey(string fontName, int fontSize)
        {
            return fontName + fontSize.ToString();
        }

        public void SetCurrentFont(string fontName, int fontSize)
        {
            if (string.IsNullOrEmpty(fontName))
            {
                fontName = "Consolas";
            }

            if (fontSize <= 0)
            {
                fontSize = 12;
            }

            string key = GenerateKey(fontName, fontSize);
            if (!_allLoadedFonts.ContainsKey(key))
            {
                FontAtlas atlas = new FontAtlas(
                        new FontImportSettings
                        {
                            FontName = fontName,
                            FontSize = fontSize
                        }
                    );
                Texture texture = new Texture(atlas.Image, new TextureImportSettings { Filtering = FilteringType.NearestNeighbour });
                _allLoadedFonts[key] = new FontAtlasTexture(atlas, texture);
            }

            _activeFont = _allLoadedFonts[key];
        }


        public float CharWidth {
            get {
                return _activeFont.FontAtlas.CharWidth;
            }
        }

        public float CharHeight {
            get {
                return _activeFont.FontAtlas.CharHeight;
            }
        }

        public float GetCharWidth(char c)
        {
            if (_activeFont.FontAtlas.IsValidCharacter(c))
            {
                return _activeFont.FontAtlas.GetCharacterSize(c).Width;
            }


            float spaceWidth = _activeFont.FontAtlas.GetCharacterSize('|').Width;

            switch (c)
            {
                case ' ':
                    return spaceWidth;
                case '\t':
                    return 4 * spaceWidth;
            }

            return 0;
        }

        public float GetCharHeight(char c)
        {
            return _activeFont.FontAtlas.GetCharacterSize(c).Height;
        }

        public SizeF GetCharSize(char c)
        {
            return _activeFont.FontAtlas.GetCharacterSize(c);
        }


        public Texture UnderlyingFontAtlas {
            get {
                return _activeFont.FontTexture;
            }
        }

        private Rect2D GetAtlasRect(char c)
        {
            return new Rect2D(0, 0, 0, 0);
        }


        //TODO: IMPLEMENT tabs and newlines
        //And vertical/horizontal aiignment features
        public void DrawText(string text, float startX, float startY, float scale = 1.0f)
        {
            CTX.SetTexture(_activeFont.FontTexture);

            float x = startX;
            float y = startY;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (_activeFont.FontAtlas.IsValidCharacter(c))
                {
                    SizeF size = GetCharSize(c);
                    Rect2D uv = _activeFont.FontAtlas.GetCharacterUV(c);

                    _geomDrawer.DrawRect(new Rect2D(x, y, x + size.Width * scale, y + size.Height * scale), uv);

                    x += size.Width * scale;
                }
                else
                {
                    x += GetCharWidth(c);

                    if(c == '\n')
                    {
                        x = startX;
                        y -= _activeFont.FontAtlas.CharHeight + 2;
                    }
                }

            }
        }

        public void Dispose()
        {
            foreach(var item in _allLoadedFonts)
            {
                item.Value.FontTexture.Dispose();
            }
        }

        public float GetStringHeight(string s)
        {
            float height = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\n')
                    height += GetCharHeight('|') + 2;
            }

            return height;
        }

        public float GetStringWidth(string s)
        {
            float maxWidth = 0;
            float width = 0;

            for (int i = 0; i < s.Length; i++)
            {
                width += GetCharWidth(s[i]);

                if (s[i] == '\n')
                    width = 0;

                maxWidth = MathF.Max(width, maxWidth);
            }

            return maxWidth;
        }
    }
}

