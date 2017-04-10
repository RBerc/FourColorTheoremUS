using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourColorTheoremUS
{
    /*
    * This class will provide a printout for the 4 Coloring Theorem of the map of the Continental US
    * The algorithm used is a modified derivation of the generic algorithm found here: https://www.cis.upenn.edu/~matuszek/cit594-2012/Pages/backtracking.html
    */
    class FourColorUS
    {
        /*
        * Using enumerations for states. Ordering derived from : http://web.stonehill.edu/compsci/cs211/Assignments/usmap.txt
        */
        enum States
        {
            Minnesota = 0,
            Utah = 1,
            Nevada = 2,
            California = 3,
            NewMexico = 4,
            Arizona = 5,
            Texas = 6,
            Colorado = 7,
            Oklahoma = 8,
            Kansas = 9,
            Oregon = 10,
            Washington = 11,
            SouthDakota = 12,
            Wyoming = 13,
            Idaho = 14,
            Nebraska = 15,
            Montana = 16,
            NorthDakota = 17,
            Illinois = 18,
            Kentucky = 19,
            Mississippi = 20,
            Louisiana = 21,
            Arkansas = 22,
            Missouri = 23,
            Tennessee = 24,
            Georgia = 25,
            Alabama = 26,
            Florida = 27,
            NorthCarolina = 28,
            SouthCarolina = 29,
            Virginia = 30,
            Pennsylvania = 31,
            Ohio = 32,
            Indiana = 33,
            WestVirginia = 34,
            Iowa = 35,
            Michigan = 36,
            Wisconsin = 37,
            DC = 38,
            Maryland = 39,
            NewYork = 40,
            Delaware = 41,
            NewJersey = 42,
            Vermont = 43,
            Connecticut = 44,
            Massachusets = 45,
            NewHampshire = 46,
            Maine = 47,
            RhodeIsland = 48
        };

        //Using more enumerations for colors
        enum Color
        {
            Yellow = 1,
            Green = 2,
            Blue = 3,
            Red = 4
        };

        static int V = 48; // Total number of vertices to be used aka
        int[] stateColor; //This will hold the color of the state(s)

        /*
        * This Method will check to see if it's safe to color the adjacent state
        * basically, if it matches an adjacent color, don't color it the current color
        */
        public bool canColor(int v, long[,] map, int[] stateColor, int c)
        {
            for (int i = 0; i < V; i++)
                if (map[v, i] == 1 && c == stateColor[i])
                    return false;
            return true;
        }

        /*
        * This will actually do the "heavy lifting" for the algorithm
        * This function is recursive and first checks the number of "colored in" states, then checks if current state can be colored. If it can, we will go through all neighbors
        */
        public bool mapColoringSolver(long[,] map, int[] stateColor, int v)
        {
            if (v == V)
                return true;
            for (int c = 1; c <= 4; c++)
            {
                if (canColor(v, map, stateColor, c))
                {
                    stateColor[v] = c;

                    if (mapColoringSolver(map, stateColor, v + 1))
                        return true;
                    stateColor[v] = 0;
                }
            }
            return false;
        }

        //This class will call the mapColoringSolver class, and determine if there is or is not a solution. 
        public bool mapColoring(long[,] map)
        {
            stateColor = new int[V];
            for (int i = 0; i < V; i++)
                stateColor[i] = 0;
            if (!mapColoringSolver(map, stateColor, 0))
            {
                Console.WriteLine("No Solution");
                return false;
            }
            showResult(stateColor);
            return true;
        }

        /*
        * Console output formatting
        * Enum String Output obtained via reference to http://stackoverflow.com/questions/309333/enum-string-name-from-value
        */
        public void showResult(int[] stateColor)
        {
            Console.WriteLine("Solution: ");
            for (int i = 0; i < V; i++)
            {
                string stateAbbr = Enum.GetName(typeof(States), i);
                string colors = Enum.GetName(typeof(Color), stateColor[i]);
                Console.WriteLine("State: " + stateAbbr + " Color:  " + colors);
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
