using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SnakeGame.Model.Utilities;
using SnakeGame.Model.GameObjects;


namespace SnakeGame.Model
{
    /// <summary>
    /// Reprezentuje grę.
    /// </summary>
    sealed class Game
    {
        /// <summary>
        /// Implementacja wzorca Singleton, w połączeniu z prywatnym konstruktorem i poziomem ochrony klasy.
        /// </summary>
        public static Game Instance { get; } = new Game();
        /// <summary>
        /// Najwyższy zdobyty wynik.
        /// </summary>
        public int HighScore { get; set; }
        /// <summary>
        /// Obecny wynik.
        /// </summary>
        private int score;
        /// <summary>
        /// Obecny wynik.
        /// </summary>
        public int Score
        {
            get { return score; }
            set
            {
                // Jeśli obecny wynik przekracza najwyższy, najwyższy wynik ustawiany jest na obecny.
                score = value;
                if (score > HighScore)
                    HighScore = value;
            }
        }
        /// <summary>
        /// Instancja levelu, w jakim odbywa się gra.
        /// </summary>
        public Level level;
        /// <summary>
        /// Lista obiektów gry należących do gry.
        /// </summary>
        public List<GameObject> gameObjects;
        /// <summary>
        /// Określa, czy gra trwa.
        /// </summary>
        public bool InGame { get; set; }
        /// <summary>
        /// Pomaga w ustawieniu liczby klatek na sekundę.
        /// </summary>
        private Clock frameTimer = new Clock();
        /// <summary>
        /// Ostatnio przechwycony klawisz.
        /// </summary>
        private ConsoleKey currentKey;
        /// <summary>
        /// Instancja interfejsu użytkownika przechowująca punkty.
        /// </summary>
        public UserInterface scoreBar;
        /// <summary>
        /// Wielkość okna konsoli.
        /// </summary>
        public Vector2Int WindowSize { get; set; }
        
        /// <summary>
        /// Prywatny konstruktor, aby nie można było utworzyć nowych instancji klasy. Inicjalizuje część zmiennych.
        /// </summary>
        private Game()
        {
            currentKey = (ConsoleKey)(-1);
            WindowSize = ConvertString.ToWindowSize(FileManager.LoadLines(Constants.iniPath));
            ConsoleManager.PrepareConsole(WindowSize);
        }
        /// <summary>
        /// Tworzy niektóre składowe obiektu Game i przygotowuje grę do działania.
        /// </summary>
        private void Initialize()
        {
            Vector2Int userInterfacePosition = Constants.UserInterfacePosition;
            Vector2Int userInterfaceSize = new Vector2Int(WindowSize.X, Constants.UserInterfaceSizeY);
            Vector2Int levelPosition = new Vector2Int(userInterfacePosition.X, userInterfacePosition.Y + userInterfaceSize.Y);
            Vector2Int levelSize = new Vector2Int(WindowSize.X, WindowSize.Y - userInterfaceSize.Y);
            currentKey = (ConsoleKey)(-1);
            Snake snake = ConvertString.ToSnake(FileManager.LoadLines(Constants.iniPath));
            Bonuses bonuses = new Bonuses();
            gameObjects = new List<GameObject> { snake, bonuses };
            level = new Level(levelPosition, levelSize);
            level.UpdateLevel(gameObjects);
            bonuses.SpawnBonus(level.PickAnEmptyPoint());
            level.UpdateLevel(gameObjects);
            Score = Constants.Score;
            HighScore = FileManager.LoadHighScore(Constants.highScorePath);
            scoreBar = new UserInterface(userInterfacePosition, userInterfaceSize, HighScore, Score);
            InGame = true;
        }
        /// <summary>
        /// Zeruje zegary węży, aby mogły prawidłowo wystartować.
        /// </summary>
        private void FireSnakes()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is Snake)
                {
                    Snake snake = gameObject as Snake;
                    snake.speedTimer.Reset();
                }
            }
        }
        /// <summary>
        /// Inicjalizuje i uruchamia grę.
        /// </summary>
        public void Run()
        {
            while (currentKey != (ConsoleKey)MenuOptions.LeaveGame)
            {
                Initialize();
                ConsoleManager.DisplayMenu(HighScore);
                currentKey = Console.ReadKey(true).Key;
                switch (currentKey)
                {
                    case (ConsoleKey)MenuOptions.StartGame:
                        {
                            ConsoleManager.ResetConsole();
                            bool actionRequest = false;
                            // Ustawienie liczby klatek na sekundę.
                            double framesPerSecond = 60;
                            TimeSpan frameTime = TimeSpan.FromSeconds(1D / framesPerSecond);
                            frameTimer.Reset();
                            FireSnakes();
                            // Główna pętla gry.
                            while (InGame && currentKey != ConsoleKey.Escape)
                            {
                                // Dopóki klawisz nie zostanie wciśnięty oraz gra trwa, wykonywana jest podpętla.
                                while (InGame && Console.KeyAvailable == false)
                                {
                                    // Aktualizacja elementów gry co interwał frameTime.
                                    if (frameTimer.TimeElapsed >= frameTime)
                                    {
                                        frameTimer.Reset();
                                        Update(actionRequest);
                                        Render();
                                        actionRequest = false;
                                    }
                                }
                                // Klawisz został wciśnięty lub gra się zakończyła.
                                // Jeśli gra się zakończyła, pętla gry zostaje przerwana.
                                if (!InGame)
                                    break;
                                // W przeciwnym razie klawisz musiał zostać wciśnięty. Przechwycenie klawisza i zakończenie iteracji.
                                actionRequest = true;
                                currentKey = Console.ReadKey(true).Key;
                            }
                            ConsoleManager.DisplayGameOverScreen(Score, HighScore);
                            while (currentKey != ConsoleKey.Enter)
                                currentKey = Console.ReadKey(true).Key;
                            ConsoleManager.ResetConsole();
                            Save();
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// Aktualizuje stan gry.
        /// </summary>
        /// <param name="checkInput">Określa, czy metoda ma obłsużyć wejście.</param>
        public void Update(bool checkInput)
        {
            bool updateDirection = false;
            Directions newDirection = Directions.Up;
            if (checkInput)
            {
                // Obsługa wejścia.
                switch (currentKey)
                {
                    case ConsoleKey.UpArrow:
                        updateDirection = true;
                        newDirection = Directions.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        updateDirection = true;
                        newDirection = Directions.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        updateDirection = true;
                        newDirection = Directions.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        updateDirection = true;
                        newDirection = Directions.Right;
                        break;
                    default:
                        break;
                }
            }
            // Aktualizacja obiektów gry.
            foreach (GameObject gameObject in gameObjects)
            {
                // Aktualizacja węży.
                if (gameObject is Snake)
                {
                    Snake snake = gameObject as Snake;
                    // Obsługa zmiany kierunku.
                    if (updateDirection)
                    {
                        Vector2Int zero = new Vector2Int(0, 0);
                        if (ConvertDirection.ToVector2Int(newDirection) + ConvertDirection.ToVector2Int(snake.Direction) != zero)
                        {
                            snake.UpdatedDirection = newDirection;
                            snake.DirectionUpdateWaiting = true;
                        }
                    }
                    // Obsługa zmiany kierunku, ruchu, zebrania bonusu i kolizji.
                    double secondsPerMove = (1D / snake.Speed);
                    int timesToMove = (int)(snake.speedTimer.TimeElapsed.TotalSeconds / secondsPerMove);
                    if (timesToMove > 0)
                    {
                        snake.UpdateDirection();
                        snake.speedTimer.SubtractTimeElapsed(TimeSpan.FromSeconds(timesToMove * secondsPerMove));
                        for (int i = 0; i < timesToMove; i++)
                        {
                            Vector2Int futureHead = snake.Points[0] + ConvertDirection.ToVector2Int(snake.Direction);
                            if (futureHead.X >= 0 && futureHead.Y >= 0 && futureHead.X < level.Size.X && futureHead.Y < level.Size.Y)
                            {
                                PointTypes pointType = level.GetPointType(futureHead);
                                if (pointType == PointTypes.Empty)
                                {
                                    // Zwykły ruch.
                                    snake.Move();
                                    level.UpdateLevel(gameObjects);
                                }
                                else if (pointType == PointTypes.Bonus)
                                {
                                    // Ruch z zebraniem bonusu i urośnięciem węża.
                                    Bonuses bonuses = level.WhatObjectIsHere(futureHead, gameObjects) as Bonuses;
                                    snake.Move(true);
                                    bonuses.DeleteBonus(futureHead);
                                    level.UpdateLevel(gameObjects);
                                    bonuses.SpawnBonus(level.PickAnEmptyPoint());
                                    level.UpdateLevel(gameObjects);
                                    Score++;
                                    scoreBar.UpdateScores(Score);
                                }
                                else
                                {
                                    // Kolizja z wężem.
                                    InGame = false;
                                }
                            }
                            else
                            {
                                // Kolizja z granicą levelu.
                                InGame = false;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Renderuje bieżącą klatkę w oknie konsoli.
        /// </summary>
        public void Render()
        {
            ConsoleManager.DrawGameView(scoreBar, level);
        }
        /// <summary>
        /// Jeśli wynik jest co najmniej równy rekordowi, zapisuje wynik do pliku jako rekord.
        /// </summary>
        public void Save()
        {
            if (Score >= HighScore)
                FileManager.SaveHighScore(Constants.highScorePath, Score);
        }
    }
}
