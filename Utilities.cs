using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities{
    static class StainGenerator2D{

        /// <summary>
        /// Generates a stain at the given origin position.
        /// </summary>
        static public List<Vector2> Generate(Vector2 origin, uint area, bool allowDiagonalSelection = true){
            List<Vector2> stain = new List<Vector2>();
            stain.Add(origin);

            Vector2 currentCell;
            Vector2 nextCell;

            bool repeatedCell;
            bool sameCell;
            bool diagonalSelection;

            int moveX;
            int moveY;

            for (int i = 0; i < area-1; i++){
                do{
                    currentCell = stain[Misc.GetRandomInt(0,stain.Count-1)];
                    moveX = Misc.GetRandomInt(-1,1);
                    moveY = Misc.GetRandomInt(-1,1);
                    nextCell = new Vector2(currentCell.x+moveX,currentCell.y+moveY);

                    repeatedCell = stain.IndexOf(nextCell) != -1;
                    sameCell = moveX == 0 && moveY == 0;
                    diagonalSelection = allowDiagonalSelection == false && moveX != 0 && moveY != 0;
                }while(repeatedCell || sameCell || diagonalSelection);
                stain.Add(nextCell);
            }
            return stain;
        }

        /// <summary>
        /// Generates a Bresenham's line from the startPoint to the endPoint.
        /// </summary>
        public static List<Vector2> GenerateBresenhamLine(Vector2 startPoint, Vector2 endPoint){
            List<Vector2> line = new List<Vector2>();

            int distanceX = Math.Abs((int)endPoint.x - (int)startPoint.x);
            int slopeX = (int)startPoint.x < (int)endPoint.x ? 1 : -1;
            
            int distanceY = Math.Abs((int)endPoint.y - (int)startPoint.y);
            int slopeY = (int)startPoint.y < (int)endPoint.y ? 1 : -1;

            int slopeError = (distanceX > distanceY ? distanceX : -distanceY) / 2;
            int e;

            while(startPoint != endPoint) {
                line.Add(startPoint);
                e = slopeError;
                if (e > -distanceX) { slopeError -= distanceY; startPoint.x += slopeX; }
                if (e < distanceY) { slopeError += distanceX; startPoint.y += slopeY; }
            }
            line.Add(endPoint);

            return line;
        }

        /// <summary>
        /// Generates a line from the startPoint to the endPoint, then stains it according to the given thickness.
        /// </summary>
        public static List<Vector2> GenerateStainedLine(Vector2 startPoint, Vector2 endPoint,uint thickness, bool allowDiagonalSelection = true){
            List<Vector2> stainedLine = GenerateBresenhamLine(startPoint,endPoint);
            Vector2[] lineCopy = stainedLine.ToArray();
            foreach (Vector2 cell in lineCopy){
                stainedLine = stainedLine.Concat<Vector2>(Generate(cell,thickness,allowDiagonalSelection)).ToList<Vector2>();
            }
            return stainedLine.Distinct<Vector2>().ToList<Vector2>();
        }

        /// <summary>
        /// Given a texture, renders the stain in the scene.
        /// </summary>
        public static void Render(List<Vector2> stain, Texture texture, Node2D world, Vector2 position){
            foreach (Vector2 cell in stain){
                Sprite sprite = new Sprite();
                sprite.Texture = texture;
                world.AddChild(sprite);
                sprite.Position = new Vector2(
                    position.x + cell.x*texture.GetWidth(),
                    position.y + cell.y*texture.GetHeight()
                );
            }
        }
    }

    static class Misc{
        static public int GetRandomInt(int min, int max){
            Random rnd = new Random();
            return rnd.Next(min,max+1);
        }
    }
}