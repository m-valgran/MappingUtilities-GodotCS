## FEB 2023 NOTE - The FOSS dilemma of using C#
<p>Avoid using C#, use Godot's built-in GDScript languaje. Reason? Well, fuck off Spycrosoft, that's the reason...</p>
<p>Whereas Godot is an open-source engine, C# is still a not fully open source product (not free/libre), and that's the problem it's a product (even if it was fully open source, would you trust it considering the fact it comes from MS and it wasn't open source in first place? Remember Visual Spyware Code for instance?).</p>
<p>Also you will get more feedback and support using GDScript, since it's the most common language in Godot. So, even though translating from GDScript to C# is not difficult task at all as long as you know the '101' syntax of any programming language, 'speaking' the predominant one will save you a lot of time.</p>
<p>Another shitty aspect of it is that making Intellisense work is a struggle. Making Intellisense work on Linux is (sometimes) a CRUSADE due to the reason that everytime the .NET mountain of spyware crap is updated, you'd probably ran into troubles to make it work again.</p>
<p>I heard comments like 'It's better to use C# because it's a widespread languaje that you can use in other areas, you can put it on your CV, blah, blah, blah'. I disagree, the challenge is not to learn a new language, one can do it in a few weeks. The real challenge is to learn the paradigms that language is used for. There are better ways to learn OOP than using Godot.</p>
<p>Conclusion: Is it worth to sell your ass to Papa Gates in order to have a typed languaje to work with? The answer is DON'T. If your design skills are good enough and if you work alone (which I feel is most likely the situation if you use Godot), GDScript should be more than enough.</p>
<p>Thanks for reading this rant.</p>
<p>Good night.</p>


# Mapping Utilities [Godot-C#]
Godot-C# library for procedural 2D grid map generation.

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
		//Here I suggest using Tilemaps with custom textures
```
### Example of random outputs
<p align="center">
	<img width="400" height="400" src="https://user-images.githubusercontent.com/47353542/158003020-0b9fb7e1-1037-4cdf-9126-c7a912780318.jpg">
	<img width="400" height="400" src="https://user-images.githubusercontent.com/47353542/158003022-da68ab6c-42e4-4eef-8834-9078ab6af0d8.jpg">
</p>
