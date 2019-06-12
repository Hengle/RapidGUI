﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace RapidGUI
{
    public class WindowLauncher : TitleContent<WindowLauncher>, IDoGUIWindow
    {
        public Rect rect;

        public bool isMoved { get; protected set; }

        public event Action<WindowLauncher> onOpen;
        public event Action<WindowLauncher> onClose;

        public bool isEnable => funcDatas.Any(data => data.checkEnableFunc?.Invoke() ?? true);


        public WindowLauncher() : base() { }

        public WindowLauncher(string name, float width = 300f) : base(name)
        {
            rect.width = width;
        }

        public WindowLauncher SetWidth(float width)
        {
            rect.width = width;
            return this;
        }

        public void DoGUI()
        {
            if (isEnable)
            {
                bool changed;
                using (new GUILayout.HorizontalScope())
                {
                    changed = isOpen != GUILayout.Toggle(isOpen, "❏ " + name, Style.toggle);
                    titleAction?.Invoke();
                }

                if (changed)
                {
                    isOpen = !isOpen;
                    if (isOpen)
                    {
                        isMoved = false;
                        rect.position = GUIUtility.GUIToScreenPoint(Event.current.mousePosition) + Vector2.right * 50f;
                        onOpen?.Invoke(this);
                    }
                    else
                    {
                        onClose?.Invoke(this);
                    }
                }

                WindowInvoker.Instance.Add(this);
            }
        }

        public void DoGUIWindow()
        {
            if (isOpen && isEnable)
            {
                var pos = rect.position;
                rect = RGUI.ResizableWindow(GetHashCode(), rect,
                    (id) =>
                    {
                        GetGUIFuncs().ForEach(func => func());
                        GUI.DragWindow();
                    }
                    , name, RGUIStyle.darkWindow);

                isMoved |= pos != rect.position;
            }
        }


        #region Style

        public static class Style
        {
            public static readonly GUIStyle toggle;
            const int leftLine = 3;

            // GUIStyleState.background will be null 
            // if it set after secound scene load and don't use a few frame
            // to keep textures, set it to other member. at unity2019
            static List<Texture2D> texList = new List<Texture2D>();


            static Style()
            {
                Color onColor = new Color(0.3f, 0.5f, 0.98f, 0.9f);

                toggle = CreateToggle(onColor);
                toggle.name = "launcher_unit_toggle";
            }

            static GUIStyle CreateToggle(Color onColor)
            {
                var style = new GUIStyle(GUI.skin.button);
                style.alignment = TextAnchor.MiddleLeft;
                //style.border = new RectOffset(0, 0, 1, underLine + 1);
                style.border = new RectOffset(leftLine + 1, 1, 0, 0);

                var bgColorHover = Vector4.one * 0.5f;
                var bgColorActive = Vector4.one * 0.7f;

                texList.Add(style.onNormal.background = CreateToggleOnTex(onColor, Color.clear));
                texList.Add(style.onHover.background = CreateToggleOnTex(onColor, bgColorHover));
                texList.Add(style.onActive.background = CreateToggleOnTex(onColor * 1.5f, bgColorActive));

                texList.Add(style.normal.background = CreateTex(Color.clear));
                texList.Add(style.hover.background = CreateTex(bgColorHover));
                texList.Add(style.active.background = CreateTex(bgColorActive));

                return style;
            }

            static Texture2D CreateToggleOnTex(Color col, Color bg)
            {
                //var tex = new Texture2D(1, underLine + 3);
                var tex = new Texture2D(leftLine + 3,1);

                for (var x = 0; x < tex.width; ++x)
                {
                    var c = (x < leftLine) ? col : bg;
                    for (var y = 0; y < tex.height; ++y)
                    {
                        //var c = (y < underLine) ? col : bg;
                        tex.SetPixel(x, y, c);
                    }
                }

                tex.Apply();

                return tex;
            }

            static Texture2D CreateTex(Color col)
            {
                var tex = new Texture2D(1, 1);
                tex.SetPixel(0, 0, col);
                tex.Apply();

                return tex;
            }
        }

        #endregion
    }
}