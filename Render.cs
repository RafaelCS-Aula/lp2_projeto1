using System;

public class Render
{
	public void Introduction()
	{
        Console.WriteLine("LP2 project, IMDB Analizer");
        Console.WriteLine("/nMade by:");
        Console.WriteLine("/tPedro Fernandes nº21803791" +
            "/n/tRafael Castro e Silva nº2190xxxx");
    }

    public void ChooseSearch()
    {
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("/n What do you intend to search? (type in the " +
            "*number* of your choice)");
        Console.WriteLine("/t1) Titles");
        // Console.WriteLine("/tX) People");
        Console.WriteLine("/n/t2) Exit 'IMDB Analizer'");
    }

    public void TitleText()
    {
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("/n Type in the *title* you're looking for");
        Console.WriteLine("(if you wish to return to 'Type Select', type " +
            "in: '_EXIT_')/n");
    }
}
