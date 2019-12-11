namespace Projeto_imdb_analyser
{
    public interface IMenuSelectable
    {
        string ScreenName {get;  set;}
        void OnSelection();

        object finalResult {get; set;}

    

    }
}