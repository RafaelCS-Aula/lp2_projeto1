using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

public class FileSearcher
{
	private string _imdbDatabase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), MyIMDBSearcher);

	private string _titleAkas = Path.Combine(_imdbDatabase, 
        "title.akas.tsv.gz");
	
	private string _titleBasics = Path.Combine(_imdbDatabase, 
        "title.basics.tsv.gz");
	
	private string _titleCrew = Path.Combine(_imdbDatabase, 
        "title.crew.tsv.gz");
	
	private string _titleEpisode = Path.Combine(_imdbDatabase, 
        "title.episode.tsv.gz");
	
	private string _titlePrincipals = Path.Combine(_imdbDatabase, 
        "title.principals.tsv.gz");
	
	private string _titleRatings = Path.Combine(_imdbDatabase, 
        "title.ratings.tsv.gz");
	
	private string _nameBasics = Path.Combine(_imdbDatabase, 
        "name.basics.tsv.gz");

	ICollection<dbItem> itemCollection;

	public void FileManager()
    {
		FileOpenner();
		FileInterpreter();
    }

    // Method used by the teacher in the example
	public void FileOpenner(string file, Action<string> actionForEachLine)
    {
        // Open Files
        using (FileStream fs = new FileStream(
            file, FileMode.Open, FileAccess.Read))
        {
            // Decorar o ficheiro com um compressor para o formato GZip
            using (GZipStream gzs = new GZipStream(
                fs, CompressionMode.Decompress))
            {
                // Usar um StreamReader para simplificar a leitura
                using (StreamReader sr = new StreamReader(gzs))
                {
                    // Linha a ler
                    string line;

                    // Ignorar primeira linha de cabeçalho
                    sr.ReadLine();

                    // Percorrer linhas
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Aplicar ação à linha atual
                        actionForEachLine.Invoke(line);
                    }
                }
            }

        }
    }

    public dbItem LineInterpreter()
    {
        // Return new dbItem
        // In the constructer, the types will all be converted to the right 
        // format (from string)
    }

}
