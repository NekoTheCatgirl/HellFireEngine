using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace HellFireEngine
{
    public static class OrderManager
    {
        /// <summary>
        /// A expensive operation that should only be run once every once in a while to calculate things.
        /// </summary>
        /// <returns>A tuple with min and max value</returns>
        public static (int, int) GetDrawOrder(IEnumerable<IDrawable> drawables)
        {
            int min = 0;
            int max = 0;

            foreach (IDrawable drawable in drawables)
            {
                if (drawable.DrawOrder < min) min = drawable.DrawOrder;
                else if (drawable.DrawOrder > max) max = drawable.DrawOrder;
            }

            return (min, max);
        }

        /// <summary>
        /// A expensive operation that should only be run once every once in a while to calculate things.
        /// </summary>
        /// <returns>A tuple with min and max value</returns>
        public static (int, int) GetUpdateOrder(IEnumerable<IUpdateable> updateables)
        {
            int min = 0;
            int max = 0;

            foreach (IUpdateable updateable in updateables)
            {
                if (updateable.UpdateOrder < min) min = updateable.UpdateOrder;
                else if (updateable.UpdateOrder > max) max = updateable.UpdateOrder;
            }

            return (min, max);
        }
    }
}
