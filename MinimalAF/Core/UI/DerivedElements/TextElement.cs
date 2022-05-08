﻿using MinimalAF.Rendering;
using System.Drawing;

namespace MinimalAF {
    public class TextElement : Element {
        public Color4 TextColor {
            get; set;
        }
        public string String { get; set; } = "";

        public string Font { get; set; } = "";
        public int FontSize { get; set; } = -1;

        float _charHeight;

        public VerticalAlignment VerticalAlignment {
            get; set;
        }
        public HorizontalAlignment HorizontalAlignment {
            get; set;
        }


        private PointF caratPos = new PointF();

        public TextElement(string text, Color4 textColor)
            : this(text, textColor, "", -1, VerticalAlignment.Bottom, HorizontalAlignment.Left) {

        }

        public TextElement(string text, Color4 textColor, VerticalAlignment vAlign = VerticalAlignment.Bottom, HorizontalAlignment hAlign = HorizontalAlignment.Left)
            : this(text, textColor, "", -1, vAlign, hAlign) {
        }

        public TextElement(string text, Color4 textColor, string fontName, int fontSize, VerticalAlignment vAlign = VerticalAlignment.Bottom, HorizontalAlignment hAlign = HorizontalAlignment.Left) {
            TextColor = textColor;
            String = text;
            VerticalAlignment = vAlign;
            HorizontalAlignment = hAlign;
            Font = fontName;
            FontSize = fontSize;
        }

        public override void OnRender() {
            if (String == null)
                return;

            SetFont(Font, FontSize);
            SetDrawColor(TextColor);

            float startX = 0, startY = 0;

            switch (VerticalAlignment) {
                case VerticalAlignment.Bottom:
                    startY = 0;
                    break;
                case VerticalAlignment.Center:
                    startY = VH(0.5f);
                    break;
                case VerticalAlignment.Top:
                    startY = VH(1.0f);
                    break;
                default:
                    break;
            }

            switch (HorizontalAlignment) {
                case HorizontalAlignment.Left:
                    startX = 0;
                    break;
                case HorizontalAlignment.Center:
                    startX = VW(0.5f);
                    break;
                case HorizontalAlignment.Right:
                    startX = VW(1f);
                    break;
                default:
                    break;
            }

            caratPos = DrawText(String, startX, startY, HorizontalAlignment, VerticalAlignment, 1);
        }

        public override Rect DefaultRect(float parentWidth, float parentHeight) {
            SetFont(Font, FontSize);
            _charHeight = GetCharHeight();

            return new Rect(0, 0, parentWidth, _charHeight + 5);
        }

        public float TextWidth() {
            return GetStringWidth(String);
        }

        public PointF GetCaratPos() {
            return caratPos;
        }

        public float GetCharacterHeight() {
            SetFont(Font, FontSize);
            return GetCharHeight();
        }
    }
}
