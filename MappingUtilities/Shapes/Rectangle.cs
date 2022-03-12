using Godot;
using System;
using System.Collections.Generic;

namespace MappingUtilities{

    sealed class Rectangle : Shape{

        public Rectangle(Vector2 area, bool fill = true) : base (Generate(area,fill)){ }

        private static List<Vector2> Generate(Vector2 area,bool fill = true){
            List<Vector2> grid = new List<Vector2>();
            for (int y = 0; y < Math.Abs(area.y); y++){
                for (int x = 0; x < Math.Abs(area.x); x++){
                    Vector2 cell = new Vector2(x*Math.Sign(area.x),y*Math.Sign(area.y));
                    if(fill){
                        grid.Add(cell);
                    }else if(y==0 || y==area.y-1 || x==0 || x==area.x-1){
                        grid.Add(cell);
                    }
                }
            }
            return grid;
        }
    }
}