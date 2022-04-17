﻿using System;

namespace MinimalAF.VisualTests.UI {

    public class DraggedContent : Element {
        Element GeneratePanel(string text) {
            return new Panel(Color4.VA(0, 0.1f), Color4.RGBA(0, 0, 1, 0.5f), Color4.RGBA(0, 1, 0, 0.5f))
                .SetChildren(
                    new TextElement(text, Color4.VA(0, 1), VerticalAlignment.Center, HorizontalAlignment.Center)
                );
        }

        public DraggedContent() {
            SetChildren(GeneratePanel("Drag component"));
        }

        UIDragTestProgram dragState;
        public override void OnMount(Window w) {
            Console.WriteLine("Mount");
            dragState = GetAncestor<UIDragTestProgram>();
        }

        public override void OnDismount() {
            Console.WriteLine("Dismount");
        }

        float startX0 = 0, startY0 = 0;
        bool isDragging = false, setNullNextFrame = false;

        public override void OnRender() { // debugging purposes
            base.OnRender();
        }

        public override void OnUpdate() {
            if (MouseStartedDragging) {
                startX0 = RelativeRect.X0;
                startY0 = RelativeRect.Y0;
                dragState.CurrentlyDraggedContent = this;
                isDragging = true;
            } else if (MouseFinishedDragging) {
                isDragging = false;
                Offset = (0, 0);
            } else if (isDragging) {
                float offsetX = startX0 + MouseDragDeltaX;
                float offsetY = startY0 + MouseDragDeltaY;

                Offset = (offsetX, offsetY);

                Console.WriteLine("" + Offset.X + ", " + Offset.Y);
            }
        }

        public override void AfterUpdate() {
            if (MouseFinishedDragging) {
                setNullNextFrame = true;
            } else if(setNullNextFrame) {
                dragState.CurrentlyDraggedContent = null;
                setNullNextFrame = false;
            }
        }

        public override void OnLayout() {
            LayoutChildren();
        }
    }

    class DragReciever : Element {
        Element GeneratePanel(string text) {
            return new OutlineRect(Color4.VA(0, 0.1f), 2)
                .SetChildren(
                    new TextElement(text, Color4.VA(0, 1), VerticalAlignment.Bottom, HorizontalAlignment.Left)
                );
        }

        public DragReciever(string text) {
            SetChildren(
                GeneratePanel(text)
            );
        }

        UIDragTestProgram dragState;
        public override void OnMount(Window w) {
            dragState = GetAncestor<UIDragTestProgram>();
        }


        public override void OnUpdate() {
            if (MouseOverSelf && MouseFinishedDragging) {
                if (dragState.CurrentlyDraggedContent != null) {
                    dragState.CurrentlyDraggedContent.Parent = this;
                    dragState.CurrentlyDraggedContent = null;
                }
            }
        }
    }

    public class UIDragTestProgram : Element {
        public DraggedContent CurrentlyDraggedContent = null;

        DraggedContent thinggo;

        public UIDragTestProgram() {
            thinggo = new DraggedContent();
            Element area1;
            SetChildren(
                area1 = new DragReciever("Area 1"),
                new DragReciever("Area 2")
            );

            area1.AddChild(thinggo);
        }

        public override void OnUpdate() {
            if (KeyPressed(KeyCode.Space)) {
                int index = thinggo.Parent.Index();

                if (index == 0) {
                    thinggo.Parent = this[1];
                } else {
                    thinggo.Parent = this[0];
                }
            }
        }

        public override void OnLayout() {
            LayoutSplit(children, Direction.Left);
            LayoutInset(children, 50);

            LayoutChildren();
        }
    }

	[VisualTest]
    public class UIDragTest : Element {
        Element container;

        public UIDragTest() {
            SetChildren(
                new UIRootElement().SetChildren(
                    container = new OutlineRect(Color4.RGBA(1, 1, 1, 1), 2)
                    .SetChildren(
                        new UIDragTestProgram()
                    )
                )
            );
        }

        public override void OnMount(Window w) {
            w.Size = (800, 600);
            w.Title = "UIAspectRatioTest";
            w.RenderFrequency = 120;
            w.UpdateFrequency = 120;

            SetClearColor(Color4.RGBA(1, 1, 1, 1));
        }

        public override void OnLayout() {
            LayoutInset(container, 10);

            LayoutChildren();
        }
    }
}
