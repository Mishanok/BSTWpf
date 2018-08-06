namespace BSTWpf.ExtensionMethods
{
    public static class HeapSort
    {
        internal static void HeapSortDo(double[] array, int count)
        {
            Heapify(array, count);

            int end = count - 1;
            while (end > 0)
            {
                Swap(array, end, 0);
                SiftDown(array, 0, --end);
            }
        }

        private static void Swap(double[] array, int a, int b)
        {
            double temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }

        private static void Heapify(double[] array, int count)
        {
            int start = count / 2 - 1;

            while (start >= 0)
            {
                SiftDown(array, start, count - 1);
                start--;
            }
        }

        private static void SiftDown(double[] array, int start, int end)
        {
            int root = start;

            while (root * 2 + 1 <= end)
            {
                int child = root * 2 + 1;
                int swap = root;
                if (array[swap] < array[child])
                {
                    swap = child;
                }
                if (child + 1 <= end && array[swap] < array[child + 1])
                {
                    swap = child + 1;
                }
                if (swap != root)
                {
                    Swap(array, root, swap);
                    root = swap;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
