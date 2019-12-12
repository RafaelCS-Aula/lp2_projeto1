namespace Projeto_imdb_analyser
{
    public class TypeValueSortOption : IMenuSelectable
    {
        private PictureTypeOptions _inputValType;
        public object finalResult 
        {get => _inputValType; set {value = _inputValType;}}

        public string ScreenName {get; set;}

        public void OnSelection()
        {
                // Tell Renderer to show prompt asking for string input
                // Then capture  that input (after making sure it's valid)
                // Store it in _inputValStr;            
        }
    }
}