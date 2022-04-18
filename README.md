# Mapping Utilities [Godot-C#]
Godot C# library for procedural grid map generation.

## Rough usage example (generate a dungeon)
```
		//generating a series of points
		Rectangle points = new Rectangle(new Vector2(7,7),false);
		points.Spread(10);

		//creating the grid that will contain the dungeon
		List<Vector2> dungeon = new List<Vector2>();

		//getting a random point as beginning
		Random rnd = new Random();
		Vector2 startPoint = points.Grid[rnd.Next(0,points.Grid.Count)];

		//drawing a line from the beginning to a next random point
		int corridorAmount = 5;
		Vector2 endPoint;
		for (int i = 0; i <= corridorAmount; i++){
			//the following line = no way for the end point to be the same as the start point
			points.Grid.Remove(startPoint);
			//selecting the end point
			endPoint = points.Grid[rnd.Next(0,points.Grid.Count)];
			//drawing a line from the start point to the end point
			Line corridor = new Line(endPoint-startPoint);
			corridor.Offset = startPoint;
			//"staining" the corridor shape
			corridor.Grid.ForEach(cell => {
				Stain stain = new Stain(6,false);
				stain.Offset = cell;
				dungeon.AddRange(stain.Grid);
			});
			//drawing a room in this point
			Stain room = new Stain(200,false);
			room.Offset = startPoint;
			//defining the current end point as the beginning, for the next iteration
			startPoint = endPoint;

			//adding both the corridor and room generated to our dungeon
			dungeon.AddRange(room.Grid);
			dungeon.AddRange(corridor.Grid);
			//adding the last room
			if(i==corridorAmount){ 
				Stain lastRoom = new Stain(200,false);
				lastRoom.Offset = startPoint;
				dungeon.AddRange(lastRoom.Grid);
			}
		}

		//removing repeated cells
		dungeon  = dungeon.Distinct().ToList();
		//generating walls
		List<Vector2> walls = new Any(dungeon).GenerateOutline();

		//rendering...
```
### Output
<p align="center">
	<img width="400" height="400" src="https://user-images.githubusercontent.com/47353542/158003020-0b9fb7e1-1037-4cdf-9126-c7a912780318.jpg">
	<img width="400" height="400" src="https://user-images.githubusercontent.com/47353542/158003022-da68ab6c-42e4-4eef-8834-9078ab6af0d8.jpg">
</p>
