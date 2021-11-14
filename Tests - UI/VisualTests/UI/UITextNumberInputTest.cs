﻿using MinimalAF.Datatypes;
using MinimalAF;
using MinimalAF.Rendering;
using MinimalAF.UI;

namespace MinimalAF.VisualTests.UI
{
    public class UITextNumberInputTest : Element
    {
        Element _root;
        Element _textInputElement;

        public override void OnStart()
        {
            Window w = GetAncestor<Window>();
            w.Size = (800, 600);
            w.Title = "Text input ui element test";

            w.RenderFrequency = 120;
            w.UpdateFrequency = 120; 120;

            CTX.SetClearColor(1, 0, 0, 1);

            _root = UICreator.CreatePanel(new Color4(1))
                .Anchors(new Rect2D(0, 0, 1, 1))
                .Offsets(new Rect2D(0, 0, 0, 0))
                .AddComponent(new UIGraphicsRaycaster());

            for (int i = 0; i < 3; i++)
            {
                float lowerAnchorX = i / 3f;
                float upperAnchorX = (i + 1f) / 3f;
                for (int j = 0; j < 3; j++)
                {
                    float lowerAnchorY = j / 3f;
                    float upperAnchorY = (j + 1f) / 3f;
                    float size = 300;
                    _root.AddChildren(
                        _textInputElement = UICreator.CreateUIElement(
                            new UIRect(new Color4(1), new Color4(0), 1),
                            new UIRectHitbox(false),
                            new UIMouseListener(),
                            new TextElement("", new Color4(0), "Comic Sans", 16, (VerticalAlignment)i, (HorizontalAlignment)j),
                            new UIMouseFeedback(new Color4(0.7f), new Color4(0.5f)),
                            new UITextFloatInput(new FloatProperty(10, -30, 29.9f, 0.5f), false)
                        )
                        .Anchors(lowerAnchorX, lowerAnchorY, upperAnchorX, upperAnchorY)
                        .Offsets(10)
                    );
                }
            }
        }

        public override void OnRender()
        {
            _root.Render(Time.DeltaTime);
        }

        public override void OnUpdate()
        {
            _root.Update(Time.DeltaTime);
        }

        public override void Resize()
        {
            base.Resize();

            _root.Resize();
        }
    }
}
