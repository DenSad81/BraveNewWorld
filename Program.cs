using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string[] mapOneDemensionalArray = File.ReadAllLines("map.txt");
        char[,] map = new char[mapOneDemensionalArray.Length, mapOneDemensionalArray[0].Length];
        int[] positionPacmanXY = new int[] { 0, 0 };
        int[] directionPacmanXY = new int[] { 0, 0 };
        char signPacman = '@';
        bool isGameRun = true;
        int quantityPoints = 0;
        int quantityCollectedPoints = 0;

        Console.CursorVisible = false;
        ConvertMap(mapOneDemensionalArray, map);
        char signBorderInMap = map[0, 0];
        int positionForPrintText = map.GetLength(0);
        positionPacmanXY = GetPositionOfHero(map, signPacman);
        FillMapOfPoint(map, ref quantityPoints);
        PrintMap(map);

        while (isGameRun)
        {
            int tempPositionForPrintText = positionForPrintText;
            tempPositionForPrintText++;

            Console.SetCursorPosition(0, tempPositionForPrintText++);
            Console.WriteLine($"Общее количество точек: {quantityPoints}");
            Console.SetCursorPosition(0, tempPositionForPrintText++);
            Console.WriteLine($"Pacman {signPacman} собрал точек: {quantityCollectedPoints}");
            Console.SetCursorPosition(0, tempPositionForPrintText++);

            ChangeDirectionOfMovingPacman(directionPacmanXY);
            MoveHero(map, positionPacmanXY, directionPacmanXY, signBorderInMap, signPacman);
            quantityCollectedPoints += CollectPoint(map, positionPacmanXY);
            isGameRun = IsGameExit();

            System.Threading.Thread.Sleep(200);
        }
    }

    static int CollectPoint(char[,] map, int[] posPacmanXY)
    {
        if (map[posPacmanXY[0], posPacmanXY[1]] == '.')
        {
            map[posPacmanXY[0], posPacmanXY[1]] = ' ';
            return 1;
        }

        return 0;
    }

    static void ChangeDirectionOfMovingPacman(int[] directionPacmanXY)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo pushKey = Console.ReadKey(true);

            switch (pushKey.Key)
            {
                case ConsoleKey.UpArrow:
                    directionPacmanXY[0] = -1;
                    directionPacmanXY[1] = 0;
                    break;

                case ConsoleKey.DownArrow:
                    directionPacmanXY[0] = 1;
                    directionPacmanXY[1] = 0;
                    break;

                case ConsoleKey.RightArrow:
                    directionPacmanXY[0] = 0;
                    directionPacmanXY[1] = 1;
                    break;

                case ConsoleKey.LeftArrow:
                    directionPacmanXY[0] = 0;
                    directionPacmanXY[1] = -1;
                    break;
            }
        }
    }

    static bool IsGameExit()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo pushKey = Console.ReadKey(true);

            if (pushKey.Key == ConsoleKey.Escape)
                return false;
        }

        return true;
    }

    static void MoveHero(char[,] map, int[] posPacmanXY, int[] dirPacmanXY, char symbolBorderInMap, char signHero)
    {

        if (map[posPacmanXY[0] + dirPacmanXY[0], posPacmanXY[1] + dirPacmanXY[1]] != symbolBorderInMap)
        {
            Console.SetCursorPosition(posPacmanXY[1], posPacmanXY[0]);
            Console.Write(' ');

            posPacmanXY[0] += dirPacmanXY[0];
            posPacmanXY[1] += dirPacmanXY[1];
        }

        Console.SetCursorPosition(posPacmanXY[1], posPacmanXY[0]);
        Console.Write(signHero);
    }

    static void ConvertMap(string[] oneDemensionalArray, char[,] dubleDemensionalArray)
    {
        for (int i = 0; i < oneDemensionalArray.Length; i++)
        {
            for (int j = 0; j < oneDemensionalArray[0].Length; j++)
                dubleDemensionalArray[i, j] = oneDemensionalArray[i][j];
        }
    }

    static void PrintMap(char[,] doobleDemensionalArray)
    {
        for (int i = 0; i < doobleDemensionalArray.GetLength(0); i++)
        {
            for (int j = 0; j < doobleDemensionalArray.GetLength(1); j++)
                Console.Write(doobleDemensionalArray[i, j]);

            Console.WriteLine();
        }
    }

    static void FillMapOfPoint(char[,] doobleDemensionalArray, ref int quantityPoint)
    {
        for (int i = 0; i < doobleDemensionalArray.GetLength(0); i++)
        {
            for (int j = 0; j < doobleDemensionalArray.GetLength(1); j++)
            {
                if (doobleDemensionalArray[i, j] == ' ')
                {
                    doobleDemensionalArray[i, j] = '.';
                    quantityPoint++;
                }
            }
        }
    }

    static int[] GetPositionOfHero(char[,] doobleDemensionalArray, char symbol)
    {
        int[] posPacmanXY = new int[2];

        for (int i = 0; i < doobleDemensionalArray.GetLength(0); i++)
        {
            for (int j = 0; j < doobleDemensionalArray.GetLength(1); j++)
            {
                if ((doobleDemensionalArray[i, j]) == symbol)
                {
                    posPacmanXY[0] = i;
                    posPacmanXY[1] = j;
                }
            }
        }

        return posPacmanXY;
    }
}