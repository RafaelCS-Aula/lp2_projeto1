using System;
using System.Collections.Generic;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Menu Renderer Interface
    /// </summary>
    public interface IMenuRenderer
    {
        /// <summary>
        /// Draw Title
        /// </summary>
        /// <param name="foreColor">Foreground Color</param>
        void DrawTitle(
            ConsoleColor foreColor = ConsoleColor.Red);

        /// <summary>
        /// Draw Loading Screen
        /// </summary>
        void DrawLoading();

        /// <summary>
        /// Draws Menu
        /// </summary>
        /// <param name="title">Menu Title</param>
        /// <param name="size">Menu Size</param>
        /// <param name="options">Menu Options</param>
        /// <param name="onEnter">On 'Enter' key Function</param>
        /// <param name="onEscape">On 'Escape' key Function</param>
        void DrawMenu(
          string title,
          Rect size,
          IEnumerable<string[]> options,
          Func<short, bool> onEnter,
          Action onEscape);

        /// <summary>
        /// Draw Results Menu
        /// </summary>
        /// <param name="onIteration">On Iteration CallBack Function</param>
        /// <param name="onDraw">On 'Menu Draw' CallBack Function</param>
        /// <param name="onKeyPress">On 'KeyPress' CallBack Function</param>
        /// <param name="totalElements">Menu Total Options Count</param>
        /// <param name="title">Menu Title</param>
        void DrawResults(
            Action<IPrintable, int, int> onIteration,
            Func<IPrintable, int, int, int> onDraw,
            Action<ConsoleKeyInfo, int> onKeyPress,
            int totalElements,
            string title
            );

        /// <summary>
        /// Draw Title Specifications
        /// </summary>
        /// <param name="title">Value Containing Title and Rating</param>
        /// <param name="cast">Dictionary of Principals</param>
        /// <param name="directors">Enumerable of Directors</param>
        /// <param name="writers">Enumerable of Writers</param>
        void DrawTitleSpecs(
            KeyValuePair<Title, Rating> title,
            IDictionary<Person, Principal> cast,
            IEnumerable<Person> directors,
            IEnumerable<Person> writers);

        /// <summary>
        /// Draw Title Specifications
        /// </summary>
        /// <param name="title">Value Containing Title and Rating of 
        /// Series</param>
        /// <param name="episodeParent">Value Containing Title and 
        /// Rating of Episode Parent</param>
        /// <param name="cast">Dictionary of Principals</param>
        /// <param name="directors">Enumerable of Directors</param>
        /// <param name="writers">Enumerable of Writers</param>
        void DrawTitleSpecs(
           KeyValuePair<Title, Rating> title,
           KeyValuePair<Title, Rating>? episodeParent,
           IDictionary<Person, Principal> cast,
           IEnumerable<Person> directors,
           IEnumerable<Person> writers);

        /// <summary>
        /// Draw Episode Specifications
        /// </summary>
        /// <param name="episode">Value Containing Episode</param>
        /// <param name="title">Value Containing Title and Rating</param>
        /// <param name="cast">Dictionary of Principals</param>
        /// <param name="directors">Enumerable of Directors</param>
        /// <param name="writers">Enumerable of Writers</param>
        void DrawEpisodeSpecs(
            KeyValuePair<string, Episode> episode,
            KeyValuePair<Title, Rating> title,
            IDictionary<Person, Principal> cast,
            IEnumerable<Person> directors,
            IEnumerable<Person> writers);

        /// <summary>
        /// Draw Person Specifications
        /// </summary>
        /// <param name="person">Person being Examined</param>
        /// <param name="knownForTitles">Dictionary of Titles</param>
        void DrawNameSpecs(
            Person person,
            IDictionary<string, Title> knownForTitles);
    }
}
