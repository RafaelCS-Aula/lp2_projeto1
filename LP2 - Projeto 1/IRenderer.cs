using System;
using System.Collections.Generic;

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
        void DrawTitleSpecs(
            KeyValuePair<Title, Rating> title,
            IDictionary<Person, Principal> cast,
            IEnumerable<Person> directors,
            IEnumerable<Person> writers);
        void DrawTitleSpecs(
           KeyValuePair<Title, Rating> title,
           KeyValuePair<Title, Rating>? episodeParent,
           IDictionary<Person, Principal> cast,
           IEnumerable<Person> directors,
           IEnumerable<Person> writers);
        void DrawEpisodeSpecs(
            KeyValuePair<string, Episode> episode,
            KeyValuePair<Title, Rating> title,
            IDictionary<Person, Principal> cast,
            IEnumerable<Person> directors,
            IEnumerable<Person> writers);
        void DrawNameSpecs(
            Person person,
            IDictionary<string, Title> knownForTitles);
    }
}
