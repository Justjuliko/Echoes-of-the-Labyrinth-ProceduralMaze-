# Maze Generator Project

This project is a **procedural maze generator** developed in Unity. It dynamically creates solvable 3D mazes with a defined entry point, exit point, and optional custom room prefabs. The maze ensures a navigable path while allowing flexibility in design and customization.

## Features
- **Customizable Maze Dimensions:** Easily adjust the width and height of the maze via the Unity Inspector.
- **Guaranteed Solvability:** A direct path is always created between the entry and exit points, ensuring the maze is solvable.
- **Room Customization:** Empty spaces in the maze can be filled with prefabs from a configurable list, with randomized rotations in 90° increments for variety.
- **Entry and Exit Points:** Dedicated entry and exit prefabs are placed at fixed locations within the maze.
- **Event-Driven Design:** A `UnityEvent` named `GenerationFinished` is triggered after the maze is fully generated and rendered, allowing for post-generation actions such as UI updates or animations.
- **Optimized Hierarchy:** All generated objects are instantiated as children of the GameObject containing the script, keeping the scene hierarchy organized.

## How It Works
1. The maze grid is initialized as a grid of walls.
2. A direct path from the start to the exit is carved to ensure the maze is solvable.
3. Additional paths are carved using the **Depth-First Search (DFS)** algorithm:
   - Random directions are chosen to carve paths from the current cell.
   - Backtracking occurs when no unvisited neighbors are available.
4. Prefabs are instantiated to render the maze:
   - **Walls** for blocked areas.
   - **Entry and exit prefabs** for the starting and ending points.
   - **Custom room prefabs** for designated empty spaces, with randomized orientations.

## How to Use
1. Add the `MazeGenerator` script to an empty GameObject in your Unity scene.
2. Assign the required prefabs for:
   - Walls (`wallPrefab`)
   - Entry point (`entryPrefab`)
   - Exit point (`exitPrefab`)
3. (Optional) Populate the `RoomPrefabs` list with custom prefabs for additional maze features.
4. Configure the maze dimensions (`width` and `height`) via the Inspector.
5. Press Play to see the maze dynamically generated in your scene.

## Future Improvements
- Support for multiple entry and exit points.
- Themed biomes or environmental variations for maze aesthetics.
- Integration of AI agents or player-controlled characters for maze exploration.
- Performance optimizations for larger mazes using Unity's SRP (Scriptable Render Pipeline).

## Example GIF
*![1204(1)](https://github.com/user-attachments/assets/80eafcc4-3350-44a7-aa7e-32649b155c22)*

---

Feel free to fork, modify, and use this project in your own games or experiments! 🚀
