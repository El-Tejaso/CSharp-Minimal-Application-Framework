﻿using MinimalAF.Datatypes;
using MinimalAF.Datatypes.Geometric;
using MinimalAF.Datatypes.UI;
using MinimalAF.Logic;
using MinimalAF.Rendering;
using MinimalAF.UI;
using MinimalAF.UI.Components.AutoResizing;
using MinimalAF.UI.Components.DataInput;
using MinimalAF.UI.Components.MouseInput;
using MinimalAF.UI.Components.Visuals;
using MinimalAF.UI.Core;
using MinimalAF.UI.Property;
using System;

namespace MinimalAF.VisualTests.UI
{
    public class UILinearArrangeTest : EntryPoint
    {
        UIElement _root;
        UIElement _textInputElement;

        public override void Start()
        {
            Window.Size = (800, 600);
            Window.Title = "Text input ui element test";

            Window.RenderFrequency = 120;
            Window.UpdateFrequency = 120;

            CTX.SetClearColor(1, 0, 0, 1);

            _root = UICreator.CreatePanel(new Color4(1))
                .AddComponent(new UIGraphicsRaycaster());

            UIElement hArrange, vArrange;

            _root.Anchors(new Rect2D(0, 0, 1, 1))
            .Offsets(new Rect2D(0, 0, 0, 0))
            .AddChildren(
                ///*
                hArrange = UICreator.CreateUIElement(
                    new UIRect(new Color4(0, 0.2f), new Color4(0,1),1),
                    new UILinearArrangement(false, false, 50, 10)
                )
                .AnchorsY(0,1)
                .OffsetsY(10,10)
                .AnchoredPosCenterX(0,0)
                .PosX(10)
                ,
                //*/
                vArrange = UICreator.CreateUIElement(
                    new UIRect(new Color4(0, 0.2f), new Color4(0, 1), 1),
                    new UILinearArrangement(true, false, -1, 10)
                )
                .AnchorsX(0.75f, 1)
                .OffsetsX(10,10)
                .AnchoredPosCenterY(0,0)
                .PosSizeY(10, 0)
            );

            ///*
            for(int i =0; i < 10; i++)
            {
                hArrange.AddChild(
                    UICreator.CreateUIElement(
                        new UIRect(new Color4(0, 0.0f), new Color4(0, 1), 1),
                        new UIText("h" + i.ToString(), new Color4(0, 1))
                    )
                );
            }
            //*/

            Random r = new Random();

            for (int i = 0; i < 10; i++)
            {
                int size = r.Next(10, 200);

                vArrange.AddChild(
                    UICreator.CreateUIElement(
                        new UIRect(new Color4(0, 0.0f), new Color4(0, 1), 1),
                        new UIText($"v{i}:h={size}", new Color4(0, 1))
                    )
                    .AnchoredPosCenterY(1,1)
                    .PosSizeY(10, size)
                );
            }
        }

        public override void Render(double deltaTime)
        {
            _root.DrawIfVisible(deltaTime);
        }

        public override void Update(double deltaTime)
        {
            _root.Update(deltaTime);
        }

        public override void Resize()
        {
            base.Resize();

            _root.Resize();
        }
    }
}
