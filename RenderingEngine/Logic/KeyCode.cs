﻿using OpenTK.Windowing.GraphicsLibraryFramework;

namespace RenderingEngine.Logic
{
    /// <summary>
    /// Currently implemented with the OpenTK.Keys enum with some minor name changes,
    /// so OpenTK.Keys may be cast into a KeyCode.
    /// 
    /// This is mainly to seperate the keycodes used here from OpenTK itself.
    /// </summary>
    public enum KeyCode
    {
        Unknown = Keys.Unknown,
        Space = Keys.Space,
        Apostrophe = Keys.Apostrophe,
        Comma = Keys.Comma,
        Minus = Keys.Minus,
        Period = Keys.Period,
        Slash = Keys.Slash,
        D0 = Keys.D0,
        D1 = Keys.D1,
        D2 = Keys.D2,
        D3 = Keys.D3,
        D4 = Keys.D4,
        D5 = Keys.D5,
        D6 = Keys.D6,
        D7 = Keys.D7,
        D8 = Keys.D8,
        D9 = Keys.D9,
        Semicolon = Keys.Semicolon,
        Equal = Keys.Equal,
        A = Keys.A,
        B = Keys.B,
        C = Keys.C,
        D = Keys.D,
        E = Keys.E,
        F = Keys.F,
        G = Keys.G,
        H = Keys.H,
        I = Keys.I,
        J = Keys.J,
        K = Keys.K,
        L = Keys.L,
        M = Keys.M,
        N = Keys.N,
        O = Keys.O,
        P = Keys.P,
        Q = Keys.Q,
        R = Keys.R,
        S = Keys.S,
        T = Keys.T,
        U = Keys.U,
        V = Keys.V,
        W = Keys.W,
        X = Keys.X,
        Y = Keys.Y,
        Z = Keys.Z,
        LeftBracket = Keys.LeftBracket,
        Backslash = Keys.Backslash,
        RightBracket = Keys.RightBracket,
        BackTick = Keys.GraveAccent,
        Escape = Keys.Escape,
        Enter = Keys.Enter,
        Tab = Keys.Tab,
        Backspace = Keys.Backspace,
        Insert = Keys.Insert,
        Delete = Keys.Delete,
        Right = Keys.Right,
        Left = Keys.Left,
        Down = Keys.Down,
        Up = Keys.Up,
        PageUp = Keys.PageUp,
        PageDown = Keys.PageDown,
        Home = Keys.Home,
        End = Keys.End,
        CapsLock = Keys.CapsLock,
        ScrollLock = Keys.ScrollLock,
        NumLock = Keys.NumLock,
        PrintScreen = Keys.PrintScreen,
        Pause = Keys.Pause,
        F1 = Keys.F1,
        F2 = Keys.F2,
        F3 = Keys.F3,
        F4 = Keys.F4,
        F5 = Keys.F5,
        F6 = Keys.F6,
        F7 = Keys.F7,
        F8 = Keys.F8,
        F9 = Keys.F9,
        F10 = Keys.F10,
        F11 = Keys.F11,
        F12 = Keys.F12,
        F13 = Keys.F13,
        F14 = Keys.F14,
        F15 = Keys.F15,
        F16 = Keys.F16,
        F17 = Keys.F17,
        F18 = Keys.F18,
        F19 = Keys.F19,
        F20 = Keys.F20,
        F21 = Keys.F21,
        F22 = Keys.F22,
        F23 = Keys.F23,
        F24 = Keys.F24,
        F25 = Keys.F25,
        Numpad0 = Keys.KeyPad0,
        Numpad1 = Keys.KeyPad1,
        Numpad2 = Keys.KeyPad2,
        Numpad3 = Keys.KeyPad3,
        Numpad4 = Keys.KeyPad4,
        Numpad5 = Keys.KeyPad5,
        Numpad6 = Keys.KeyPad6,
        Numpad7 = Keys.KeyPad7,
        Numpad8 = Keys.KeyPad8,
        Numpad9 = Keys.KeyPad9,
        NumpadDecimal = Keys.KeyPadDecimal,
        NumpadDivide = Keys.KeyPadDivide,
        NumpadMultiply = Keys.KeyPadMultiply,
        NumpadSubtract = Keys.KeyPadSubtract,
        NumpadAdd = Keys.KeyPadAdd,
        NumpadEnter = Keys.KeyPadEnter,
        NumpadEqual = Keys.KeyPadEqual,
        LeftShift = Keys.LeftShift,
        LeftControl = Keys.LeftControl,
        LeftAlt = Keys.LeftAlt,
        LeftSuper = Keys.LeftSuper,
        RightShift = Keys.RightShift,
        RightControl = Keys.RightControl,
        RightAlt = Keys.RightAlt,
        RightSuper = Keys.RightSuper,
        Menu = Keys.Menu,
        LastKey = Keys.LastKey,
    }
}
