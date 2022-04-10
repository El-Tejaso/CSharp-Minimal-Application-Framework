﻿using System;
using System.Collections.Generic;

namespace MinimalAF.VisualTests.UI {
	[VisualTest]
    public class UIGoldenRatioTest : Element {

        class SplitContainer : Element {
            Panel GeneratePanel(Color4 col) {
                return new Panel(Color4.VA(0, 0.1f), col, Color4.RGBA(0, 1, 0, 0.5f));
            }

            LayoutDirection _dir;

            public SplitContainer(LayoutDirection dir) {
                _dir = dir;

                SetChildren(
                    GeneratePanel(Color4.RGBA(1, 0, 0, 0.5f)),
                    GeneratePanel(Color4.RGBA(0, 0, 1, 0.5f))
                );
            }

            float[] goldenRatioSplit = new float[] { 0, 0.38196601125f, 1 };


            public override void OnLayout() {
                LayoutLinear(_children, _dir, goldenRatioSplit, true);
                LayoutInset(_children, 10);

                LayoutChildren();
            }
        }

        public UIGoldenRatioTest() {
            SetChildren(
                new UIRootElement()
            );

            Element container = this[0];

            for (int i = 0; i < 5; i++) {
                var right = new SplitContainer(LayoutDirection.Right);
                var down = new SplitContainer(LayoutDirection.Down);
                var left = new SplitContainer(LayoutDirection.Left);
                var up = new SplitContainer(LayoutDirection.Up);

                container.SetChildren(right);
                right[1].SetChildren(down);
                down[1].SetChildren(left);
                left[1].SetChildren(up);

                container = up[1];
            }
        }

        public override void OnMount(Window w) {

            w.Size = (800, 600);
            w.Title = "UI Golden ratio test Test";
            w.RenderFrequency = 120;

            SetClearColor(Color4.RGBA(1, 1, 1, 1));
        }
    }
}