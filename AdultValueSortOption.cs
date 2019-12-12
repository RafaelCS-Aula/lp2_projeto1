namespace Projeto_imdb_analyser
{
    public class AdultValueSortOption : IMenuSelectable
    {
        private bool _inputValBool;
        public object finalResult 
        {get => _inputValBool; set {value = _inputValBool;}}

        public string ScreenName {get; set;}

        public void OnSelection()
        {
                // Tell Renderer to show prompt asking for string input
                // Then capture  that input (after making sure it's valid)
                // Store it in _inputValStr;            
        }        
    }
}