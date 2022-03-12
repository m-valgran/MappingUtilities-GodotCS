using Godot;
using System;
using System.Collections.Generic;

namespace MappingUtilities{
    
    sealed class Line : Shape{
        
        public Line(Vector2 endPos) : base (Generate(endPos)){ }

        private static List<Vector2> Generate(Vector2 endPos){
            List<Vector2> line = new List<Vector2>();
            Vector2 startPos = Vector2.Zero;

            int distanceX = Math.Abs((int)endPos.x - (int)startPos.x);
            int slopeX = (int)startPos.x < (int)endPos.x ? 1 : -1;
            
            int distanceY = Math.Abs((int)endPos.y - (int)startPos.y);
            int slopeY = (int)startPos.y < (int)endPos.y ? 1 : -1;

            int slopeError = (distanceX > distanceY ? distanceX : -distanceY) / 2;
            int e;

            while(startPos != endPos) {
                line.Add(startPos);
                e = slopeError;
                if (e > -distanceX) { slopeError -= distanceY; startPos.x += slopeX; }
                if (e < distanceY) { slopeError += distanceX; startPos.y += slopeY; }
            }
            line.Add(endPos);

            return line;
        }
    }
}