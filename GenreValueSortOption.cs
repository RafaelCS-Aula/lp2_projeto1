namespace Projeto_imdb_analyser
{
    public class GenreValueSortOption : IMenuSelectable
    {
        private GenreOptions?[] _inputValGnr = new GenreOptions?[3];
        public object finalResult 
        {get => _inputValGnr; set {value = _inputValGnr;}}

        public string ScreenName {get; set;}

        public void OnSelection()
        {
                // Tell Renderer to show the list with all the genres
                // Then capture  that input (after making sure it's valid)
                // Store it in _inputValStr;            
        }        
    }
}