using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        const ConsoleKey MoveUpCommand = ConsoleKey.UpArrow;
        const ConsoleKey MoveDownCommand = ConsoleKey.DownArrow;
        const ConsoleKey MoveLeftCommand = ConsoleKey.LeftArrow;
        const ConsoleKey MoveRightCommand = ConsoleKey.RightArrow;
        const ConsoleKey ExitCommand = ConsoleKey.Escape;

        string[] mapOneDemensionalArray = File.ReadAllLines("map.txt");
        char[,] map = new char[mapOneDemensionalArray.Length, mapOneDemensionalArray[0].Length];
        int[] positionPacman = new int[] { 0, 0 };
        int[] directionPacman = new int[] { 0, 0 };
        char signPacman = '@';
        char signPoint = '.';
        char signEmpty = ' ';
        bool isGameRun = true;
        int quantityPoints = 0;
        int quantityCollectedPoints = 0;

        Console.CursorVisible = false;
        ConvertMap(mapOneDemensionalArray, map);
        char signBorderInMap = map[0, 0];
        int positionForPrintText = map.GetLength(0);
        positionPacman = GetPositionOfPacman(map, signPacman);
        FillMapOfPoints(map, ref quantityPoints);
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

            ChangeDirectionOfMovingPacman(directionPacman, MoveUpCommand, MoveDownCommand, MoveRightCommand, MoveLeftCommand);
            MoveHero(map, positionPacman, directionPacman, signBorderInMap, signPacman, signEmpty);
            quantityCollectedPoints += CollectPoint(map, positionPacman, signPoint, signEmpty);
            isGameRun = IsGameExit(ExitCommand);

            System.Threading.Thread.Sleep(200);
        }
    }

    static int CollectPoint(char[,] map, int[] posPacmanXY, char signPoint, char signEmpty)
    {
        if (map[posPacmanXY[0], posPacmanXY[1]] == signPoint)
        {
            map[posPacmanXY[0], posPacmanXY[1]] = signEmpty;
            return 1;
        }

        return 0;
    }

    static void ChangeDirectionOfMovingPacman(int[] directionPacman, ConsoleKey Up, ConsoleKey Down, ConsoleKey Right, ConsoleKey Left)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo pushKey = Console.ReadKey(true);

            // directionPacman[0] = 0; //если сделать так, то при нажатии любой клавиши,
            // directionPacman[1] = 0; //отличной от Up, Down, Right, Left пакмен будет останавливаться

            if (pushKey.Key == Up)
            {
                directionPacman[0] = -1;
                directionPacman[1] = 0;// directionPacman[1] = 0;
            }
            else if (pushKey.Key == Down)
            {
                directionPacman[0] = 1;
                directionPacman[1] = 0;// directionPacman[1] = 0;
            }
            else if (pushKey.Key == Right)
            {
                directionPacman[0] = 0;// directionPacman[0] = 0;
                directionPacman[1] = 1;
            }
            else if (pushKey.Key == Left)
            {
                directionPacman[0] = 0;// directionPacman[0] = 0;
                directionPacman[1] = -1;
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

    static void MoveHero(char[,] map, int[] positionPacman, int[] directionPacman, char symbolBorderInMap, char signHero, char signEmpty)
    {

        if (map[positionPacman[0] + directionPacman[0], positionPacman[1] + directionPacman[1]] != symbolBorderInMap)
        {
            Console.SetCursorPosition(positionPacman[1], positionPacman[0]);
            Console.Write(signEmpty);

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

    static void FillMapOfPoints(char[,] doobleDemensionalArray, ref int quantityPoint)
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

    static int[] GetPositionOfPacman(char[,] doobleDemensionalArray, char symbol)
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