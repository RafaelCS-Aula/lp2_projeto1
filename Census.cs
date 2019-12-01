using System;

public class Census
{
    /* There might be a way to make these methods's structure 'universal' for 
     * evaluating the user's input. 
     * 
     * Perhaps by giving arguments that work as 'control variables' inside the 
     * method?
    */

    // For 'Search Type'
	public int SearchControlS()
	{
        bool validAnswer = false;

        while (!validAnswer)
        {
            Console.Write("Write your response: ");
            try
            {
                int typeAnswer = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Input, try again");

                int typeAnswer = 0;
            }
            finally
            {
                if (1 <= typeAnswer <= 2)
                {
                    validAnswer = true;
                }
                else
                {
                    Console.WriteLine("/nWrite a number that corresponds " +
                        "to the presented options");
                }
            }
        }
        return typeAnswer;
    }

    // For 'Titles'
    public string SearchControlT()
    {
        bool validAnswer = false;

        while (!validAnswer)
        {
            Console.Write("Title: ");
            try
            {
                string titleAnswer = Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Input, try again");
                string titleAnswer = "_exception_";
            }
            finally
            {
                if (titleAnswer != "_exception_")
                {
                    validAnswer = true;
                }
            }
        }
        return titleAnswer;
    }
}
