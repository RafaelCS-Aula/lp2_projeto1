namespace Projeto_imdb_analyser
{
    sealed class TitleValueSortOption: IMenuSelectable
    {

        private string _inputValStr;
        public object finalResult 
        {get => _inputValStr as string; set {value = _inputValStr;}}

        public string ScreenName {get; set;}

        public void OnSelection()
        {
                // Tell Renderer to show prompt asking for string input
                // Then capture  that input (after making sure it's valid)
                // Store it in _inputValStr;            
        }
    }
}