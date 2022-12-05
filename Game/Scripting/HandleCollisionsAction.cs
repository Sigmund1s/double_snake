using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with the food, or the snake collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool isGameOver = false;
        private bool isGameOver2 = false;
        private int count = 0;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (isGameOver == false && isGameOver2 == false)
            {
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// Sets the game over flag if the snake collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            Score score = (Score)cast.GetFirstActor("score");
            Snake snake = (Snake)cast.GetFirstActor("snake");
            Snake2 snake2 = (Snake2)cast.GetFirstActor("snake2");
            Actor head = snake.GetHead();
            Actor head2 = snake2.GetHead();
            List<Actor> body = snake.GetBody();
            List<Actor> body2 = snake2.GetBody();

            foreach (Actor segment in body)
            {
                if (segment.GetPosition().Equals(head2.GetPosition()))
                {
                    isGameOver = true;
                    score.AddPoints(1,0);
                }
            }
            foreach (Actor segment in body2)
            {
                if (segment.GetPosition().Equals(head.GetPosition()))
                {
                    isGameOver2 = true;
                    score.AddPoints(0,1);
                }
            }
        }

        private void HandleGameOver(Cast cast)
        {
            this.count++;
            Snake snake = (Snake)cast.GetFirstActor("snake");
            Snake2 snake2 = (Snake2)cast.GetFirstActor("snake2");
            List<Actor> segments = snake.GetSegments();
            List<Actor> segments2 = snake2.GetSegments();
            if (isGameOver == true)
            {
                // create a "game over" message
                int x = Constants.MAX_X / 6;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                message.SetText("Game Over! Player 1 Won!");
                message.SetPosition(position);
                message.SetFontSize(50);
                cast.AddActor("messages", message);

                // make everything white
                foreach (Actor segment in segments)
                {
                    segment.SetColor(Constants.WHITE);
                }
                foreach(Actor segment in segments2){
                    segment.SetColor(Constants.WHITE);
                }
            }
            else if (isGameOver2 == true)
            {
                // create a "game over" message
                int x = Constants.MAX_X / 6;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                message.SetText("Game Over! Player 2 Won!");
                message.SetPosition(position);
                message.SetFontSize(50);
                cast.AddActor("messages", message);

                // make everything white
                foreach (Actor segment in segments)
                {
                    segment.SetColor(Constants.WHITE);
                }
                foreach(Actor segment in segments2){
                    segment.SetColor(Constants.WHITE);
                }
            }
            else if (count == 60 && !isGameOver && !isGameOver2){
                snake.GrowTail(1);
                snake2.GrowTail(1);
                count = 0;
            }
        }

    }
}