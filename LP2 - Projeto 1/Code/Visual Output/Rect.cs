namespace LP2___Projeto_1
{
    /// <summary>
    /// Struct for Rect
    /// </summary>
    public struct Rect
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        /// <summary>
        /// Constructor, sets Rect Position and Dimensions
        /// </summary>
        /// <param name="x">Top Left Position in 'x' axis</param>
        /// <param name="y">Top Left Position in 'y' axis</param>
        /// <param name="width">Rectangle Width</param>
        /// <param name="height">Rectangle Height</param>
        public Rect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
