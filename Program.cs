using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string[] mapOneDemensionalArray = File.ReadAllLines("map.txt");
        char[,] map = new char[mapOneDemensionalArray.Length, mapOneDemensionalArray[0].Length];
        int positionPacmanX;
        int positionPacmanY;
        int directionPacmanX = 0;
        int directionPacmanY = 0;
        char signPacman = '@';
        bool isGameRun = true;
        int quantityPoint = 0;
        int quantityPointAskuisitionPacman = 0;

        Console.CursorVisible = false;
        ConvertMap(mapOneDemensionalArray, map);
        char signBorderInMap = map[0, 0];
        int positionForPrintText = map.GetLength(0);
        GetPositionOfHero(map, signPacman, out positionPacmanX, out positionPacmanY);
        FillMapOfPoint(map, ref quantityPoint);
        PrintMap(map);

        while (isGameRun)
        {
            int tempPositionForPrintText = positionForPrintText;
            tempPositionForPrintText++;

            Console.SetCursorPosition(0, tempPositionForPrintText++);
            Console.WriteLine($"Общее количество точек: {quantityPoint}");
            Console.SetCursorPosition(0, tempPositionForPrintText++);
            Console.WriteLine($"Pacman {signPacman} собрал точек: {quantityPointAskuisitionPacman}");
            Console.SetCursorPosition(0, tempPositionForPrintText++);

            GetDirectionOfMovingPacman(ref directionPacmanX, ref directionPacmanY);
            MoveHero(map, ref positionPacmanX, directionPacmanX, ref positionPacmanY, directionPacmanY, signBorderInMap, signPacman);
            AsquizitionOfPoint(map, positionPacmanX, positionPacmanY, ref quantityPointAskuisitionPacman);
            isGameRun = IsGameExit(isGameRun);

            System.Threading.Thread.Sleep(200);
        }
    }

    static void AsquizitionOfPoint(char[,] map, int positionX, int positionY, ref int quantityPointAsk)
    {
        if (map[positionX, positionY] == '.')
        {
            map[positionX, positionY] = ' ';
            quantityPointAsk++;
        }
    }

    static void GetDirectionOfMovingPacman(ref int directionX, ref int directionY)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo pushKey = Console.ReadKey(true);

            switch (pushKey.Key)
            {
                case ConsoleKey.UpArrow:
                    directionX = -1;
                    directionY = 0;
                    break;

                case ConsoleKey.DownArrow:
                    directionX = 1;
                    directionY = 0;
                    break;

                case ConsoleKey.RightArrow:
                    directionX = 0;
                    directionY = 1;
                    break;

                case ConsoleKey.LeftArrow:
                    directionX = 0;
                    directionY = -1;
                    break;
            }
        }
    }

    static bool IsGameExit(bool isGameRun)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo pushKey = Console.ReadKey(true);

            if (pushKey.Key == ConsoleKey.Escape)
                isGameRun = false;
        }

        return isGameRun;
    }

    static void MoveHero(char[,] map, ref int positionX, int directionX, ref int positionY, int directionY, char symbolBorderInMap, char signHero)
    {
        if (map[positionX + directionX, positionY + directionY] != symbolBorderInMap)
        {
            Console.SetCursorPosition(positionY, positionX);
            Console.Write(' ');

            positionX += directionX;
            positionY += directionY;
        }

        Console.SetCursorPosition(positionY, positionX);
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

    static void GetPositionOfHero(char[,] doobleDemensionalArray, char symbol, out int positionX, out int positionY)
    {
        positionX = 0;
        positionY = 0;

        for (int i = 0; i < doobleDemensionalArray.GetLength(0); i++)
        {
            for (int j = 0; j < doobleDemensionalArray.GetLength(1); j++)
            {
                if ((doobleDemensionalArray[i, j]) == symbol)
                {
                    positionX = i;
                    positionY = j;
                }
            }
        }
    }
}