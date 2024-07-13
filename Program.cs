using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        const ConsoleKey Up = ConsoleKey.UpArrow;
        const ConsoleKey Down = ConsoleKey.DownArrow;
        const ConsoleKey Left = ConsoleKey.LeftArrow;
        const ConsoleKey Right = ConsoleKey.RightArrow;
        const ConsoleKey Exit = ConsoleKey.Escape;

        string[] mapOneDemensionalArray = File.ReadAllLines("map.txt");
        char[,] map = new char[mapOneDemensionalArray.Length, mapOneDemensionalArray[0].Length];
        int[] positionPacman = new int[] { 0, 0 };
        int[] directionPacman = new int[] { 0, 0 };
        char signPacman = '@';
        bool isGameRun = true;
        int quantityPoints = 0;
        int quantityCollectedPoints = 0;

        Console.CursorVisible = false;
        ConvertMap(mapOneDemensionalArray, map);
        char signBorderInMap = map[0, 0];
        int positionForPrintText = map.GetLength(0);
        positionPacman = GetPositionOfHero(map, signPacman);
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

            ChangeDirectionOfMovingPacman(directionPacman, Up, Down, Right, Left);
            MoveHero(map, positionPacman, directionPacman, signBorderInMap, signPacman);
            quantityCollectedPoints += CollectPoint(map, positionPacman);
            isGameRun = IsGameExit(Exit);

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

    static void ChangeDirectionOfMovingPacman(int[] directionPacmanXY, ConsoleKey Up, ConsoleKey Down, ConsoleKey Right, ConsoleKey Left)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo pushKey = Console.ReadKey(true);

            if (pushKey.Key == Up)
            {
                directionPacmanXY[0] = -1;
                directionPacmanXY[1] = 0;
            }
            else if (pushKey.Key == Down)
            {
                directionPacmanXY[0] = 1;
                directionPacmanXY[1] = 0;
            }
            else if (pushKey.Key == Right)
            {
                directionPacmanXY[0] = 0;
                directionPacmanXY[1] = 1;
            }
            else if (pushKey.Key == Left)
            {
                directionPacmanXY[0] = 0;
                directionPacmanXY[1] = -1;
            }
        }
    }

    static bool IsGameExit(ConsoleKey Exit)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo pushKey = Console.ReadKey(true);

            if (pushKey.Key == Exit)
                return false;
        }

        return true;
    }

    static void MoveHero(char[,] map, int[] positionPacman, int[] directionPacman, char symbolBorderInMap, char signHero)
    {

        if (map[positionPacman[0] + directionPacman[0], positionPacman[1] + directionPacman[1]] != symbolBorderInMap)
        {
            Console.SetCursorPosition(positionPacman[1], positionPacman[0]);
            Console.Write(' ');

            positionPacman[0] += directionPacman[0];
            positionPacman[1] += directionPacman[1];
        }

        Console.SetCursorPosition(positionPacman[1], positionPacman[0]);
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
        int[] positionPacman = new int[2];

        for (int i = 0; i < doobleDemensionalArray.GetLength(0); i++)
        {
            for (int j = 0; j < doobleDemensionalArray.GetLength(1); j++)
            {
                if ((doobleDemensionalArray[i, j]) == symbol)
                {
                    positionPacman[0] = i;
                    positionPacman[1] = j;
                }
            }
        }

        return positionPacman;
    }
}