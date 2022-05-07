using System;
using System.Collections.ObjectModel;

namespace Project
{
    public interface IBubblesGrid
    {
        /// <summary>
        /// The collection of IBubbles in the grid
        /// </summary>
        /// <returns>the collection</returns>
        Collection<IBubble> GetBubbles();

        /// <summary>
        /// The Y coordinate of the lowest row
        /// </summary>
        /// <returns>The coordinate</returns>
        double GetLastRowY();

        /// <summary>
        /// The state of the lowest bubble in the grid
        /// </summary>
        /// <returns>true if the lowest bubble is at the end of the grid</returns>
        bool EndReached();

        
        /// <summary>
        /// Adds a new bubble to the grid
        /// </summary>
        /// <param name="b">the new bubble to add</param>
        void AddBubble(IBubble b);

        
        /// <summary>
        /// Removes the bubble in the position
        /// </summary>
        /// <param name="p">the position of the bubble to remove</param>
        void RemoveBubble(Tuple<double,double> p);

        /// <summary>
        /// Checks if the bubble is attachable to the grid
        /// </summary>
        /// <param name="b">the bubble to attach</param>
        /// <returns>true if the bubble is attachable</returns>
        bool IsBubbleAttachable(IBubble b);

        /// <summary>
        /// The same color neighbors as the bubble in input
        /// </summary>
        /// <param name="b">The bubble to start the search for neighbors</param>
        /// <returns>the collection of neighbor with the same color as b</returns>
        Collection<IBubble> GetSameColorNeighbors(IBubble b);

        /// <summary>
        /// Moves the grid down by rows
        /// </summary>
        /// <param name="rows">the number of rows to lower</param>
        void MoveBubblesDown(int rows);

        /// <summary>
        /// An unconnected bubble is a bubble that isn't connected to the top of the grid nor their neighbors are
        /// </summary>
        /// <returns>The collection of unconnected bubbles</returns>
        Collection<IBubble> CheckForUnconnectedBubbles();

        /// <summary>
        /// Removes the bubble and all floating bubbles present after the removal
        /// </summary>
        /// <param name="p">The position of the bubble to remove</param>
        void RemoveBubblesCascading(Tuple<double, double> p);

        /// <summary>
        /// Removes all unconnected bubbles present
        /// </summary>
        void RemoveUnconnectedBubbles();

    }

}