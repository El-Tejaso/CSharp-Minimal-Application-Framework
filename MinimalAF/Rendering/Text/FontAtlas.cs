﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;

namespace MinimalAF.Rendering.Text {
    //taken from https://gamedev.stackexchange.com/questions/123978/c-opentk-text-rendering
    public class FontImportSettings {
        public static string Text = "GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);";

        //Font import settings
        public int FontSize = 14;
        public bool BitmapFont = false;
        public string FromFile; //= "joystix monospace.ttf";
        public string FontName = "Consolas";

        //Atlas settings
        public int Padding = 2;
    }

    //concept taken from https://gamedev.stackexchange.com/questions/123978/c-opentk-text-rendering
    public class FontAtlas {
        public const string DefaultCharacters = "!#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~'";
        private Font systemFont;
        private Bitmap bitmap;
        private Dictionary<char, Rect> characterQuadCoords;

        public Font SystemFont {
            get => systemFont;
        }
        public Bitmap Image {
            get => bitmap;
        }
        public float CharWidth {
            get; internal set;
        }
        public float CharHeight {
            get; internal set;
        }

        FontImportSettings importSettings;

        /// <summary>
        /// May be different to what was specified.
        /// </summary>
        public string FontName => importSettings.FontName;
        public int FontSize => importSettings.FontSize;

        public static FontAtlas CreateFontAtlas(FontImportSettings importSettings, string characters = DefaultCharacters) {
            Font systemFont = TryGenerateSystemFontObject(importSettings);
            if (systemFont == null)
                return null;

            return new FontAtlas(importSettings, systemFont, characters);
        }

        private FontAtlas(FontImportSettings importSettings, Font font, string characters) {
            this.importSettings = importSettings;
            importSettings.FontName = font.Name;

            //Used to handle the error of invalid characters being looked up
            if (!characters.Contains('?'))
                characters += '?';

            this.systemFont = font;

            int padding = importSettings.Padding;

            bitmap = CreateAtlasBaseImage(importSettings, characters, font, padding);

            characterQuadCoords = new Dictionary<char, Rect>();

            RenderAtlas(importSettings, characters, font, characterQuadCoords, padding, bitmap);
        }

        public Rect GetCharacterUV(char c) {
            if (!IsValidCharacter(c)) {
                c = '?';
            }

            return characterQuadCoords[c];
        }

        public SizeF GetCharacterSize(char c) {
            Rect normalized = GetCharacterUV(c);
            float width = bitmap.Width;
            float height = bitmap.Height;

            return new SizeF(
                normalized.Width * width,
                normalized.Height * height
                );
        }

        public bool IsValidCharacter(char c) {
            return characterQuadCoords.ContainsKey(c);
        }

        private static Font TryGenerateSystemFontObject(FontImportSettings fontSettings) {
            Font font;

            if (!string.IsNullOrWhiteSpace(fontSettings.FromFile)) {
                var collection = new PrivateFontCollection();
                collection.AddFontFile(fontSettings.FromFile);
                var fontFamily = new FontFamily(Path.GetFileNameWithoutExtension(fontSettings.FromFile), collection);
                font = new Font(fontFamily, fontSettings.FontSize);
            } else {
                try {
                    font = new Font(new FontFamily(fontSettings.FontName), fontSettings.FontSize);
                } catch {
                    return null;
                }
            }

            return font;
        }

        private void RenderAtlas(FontImportSettings fontSettings, string characters, Font font, Dictionary<char, Rect> coordMap, int padding, Bitmap bitmap) {
            using (var g = Graphics.FromImage(bitmap)) {
                ConfigureGraphicsWithFontSettings(fontSettings, g);
                g.Clear(Color.FromArgb(0, 0, 0, 0));

                float currentY = padding;

                for (int i = 0; i < characters.Length; i++) {
                    char c = characters[i];
                    SizeF size = GetCharcterSize(fontSettings, font, g, c);

                    float currentX = padding;

                    g.DrawString(c.ToString(), font, Brushes.White, currentX, currentY, StringFormat.GenericTypographic);

                    float u0 = currentX / bitmap.Width;
                    float v0 = currentY / bitmap.Height;
                    float u1 = (currentX + size.Width) / bitmap.Width;
                    float v1 = (currentY + size.Height) / bitmap.Height;

                    Rect uv = new Rect(u0, v0, u1, v1).Rectified();

                    coordMap[c] = uv;

                    currentY += size.Height + padding;
                }
            }
        }

        private Bitmap CreateAtlasBaseImage(FontImportSettings fontSettings, string characters, Font font, int padding) {
            int bitmapWidth = 0;
            int bitmapHeight = 0;
            CalculateImageDimensions(fontSettings, characters, font, padding, out bitmapWidth, out bitmapHeight);

            Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            return bitmap;
        }

        //Assumes that the letters will be vertically packed
        private void CalculateImageDimensions(FontImportSettings fontSettings, string characters, Font font, int padding, out int bitmapWidth, out int bitmapHeight) {
            float height = padding;
            float width = 0;

            float maxWidth = 0;
            float maxHeight = 0;

            using (var g = Graphics.FromImage(new Bitmap(1, 1))) {
                ConfigureGraphicsWithFontSettings(fontSettings, g);

                for (int i = 0; i < characters.Length; i++) {
                    char c = characters[i];
                    SizeF size = GetCharcterSize(fontSettings, font, g, c);

                    width = MathF.Max(width, size.Width);
                    height += size.Height + padding;

                    maxWidth = MathF.Max(maxWidth, size.Width);
                    maxHeight = MathF.Max(maxHeight, size.Height);
                }
            }

            bitmapWidth = (int)width + 2 * padding;
            bitmapHeight = (int)height;

            CharWidth = maxWidth;
            CharHeight = maxHeight;
        }

        private SizeF GetCharcterSize(FontImportSettings fontSettings, Font font, Graphics g, char c) {
            return g.MeasureString(c.ToString(), font, fontSettings.FontSize, StringFormat.GenericTypographic);
        }

        private void ConfigureGraphicsWithFontSettings(FontImportSettings fontSettings, Graphics g) {
            g.PageUnit = GraphicsUnit.Pixel;
            g.PageScale = 1.0f;

            if (fontSettings.BitmapFont) {
                g.SmoothingMode = SmoothingMode.None;
                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            } else {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            }
        }
    }
}
