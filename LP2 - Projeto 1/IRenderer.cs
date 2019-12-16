﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LP2___Projeto_1
{
    public interface IMenuRenderer
    {
        void DrawTitle(
            ConsoleColor foreColor = ConsoleColor.Red);
        void DrawLoading();
        void DrawMenu(
          string title,
          Rect size,
          IEnumerable<string[]> options,
          Func<short, bool> onEnter,
          Action onEscape);
        void DrawResults(
            Action<IPrintable, int, int> onIteration,
            Func<IPrintable, int, int, int> onDraw,
            Action<ConsoleKeyInfo, int> onKeyPress,
            int totalElements,
            string title
            );
    }
}
