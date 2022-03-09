using Godot;
using System;
using System.Collections.Generic;

namespace MappingUtilities{
    
    sealed class Stain : Shape{

        public Stain(uint area, bool allowSlopes = true) : base (Generate(area,allowSlopes)){ }

        private static List<Vector2> Generate(uint area, bool allowSlopes = true){
            List<Vector2> grid = new List<Vector2>();
            grid.Add(new Vector2(0,0));

            Vector2 currentCell;
            Vector2 nextCell;

            bool repeatedCell;
            bool sameCell;
            bool slope;

            int moveX;
            int moveY;

            Random rnd = new Random();

            for (int i = 0; i < area-1; i++){
                do{
                    currentCell = grid[rnd.Next(0,grid.Count)];
                    moveX = rnd.Next(-1,2);
                    moveY = rnd.Next(-1,2);
                    nextCell = new Vector2(currentCell.x+moveX,currentCell.y+moveY);

                    repeatedCell = grid.IndexOf(nextCell) != -1;
                    sameCell = moveX == 0 && moveY == 0;
                    slope = allowSlopes == false && moveX != 0 && moveY != 0;
                }while(repeatedCell || sameCell || slope);
                grid.Add(nextCell);
            }
            return grid;
        }
    }
}