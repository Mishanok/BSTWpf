using System;
using BSTWpf.Model;

namespace BSTWpf.ExtensionMethods
{
    public static class TreeHelpMethods
    {
        #region WriteTree

        public static string WriteAgain(this Node node, ref string result)
        {
            if (node != null)
            {
                WriteAgain(node.Left, ref result);
                result += node.Value.ToString() + " ";
                WriteAgain(node.Right, ref result);
            }
            return result;
        }

        public static string GetString(this Tree tree)
        {
            string result = "";
            return WriteAgain(tree.Root, ref result);
        }
        #endregion
        #region MakeTree
        public static void MakeTree(this Tree tree, double[] arr)
        {
                arr.Sort();
                tree.Root = CreateBalancedTree(arr, 0, arr.Length - 1);
        }

        private static Node CreateBalancedTree(double[] arr, int start, int end)
        {
            if (end < start) return null;

            var mid = (start + end) / 2;
            var node = new Node(arr[mid]);

            node.Left = CreateBalancedTree(arr, start, mid - 1);
            node.Right = CreateBalancedTree(arr, mid + 1, end);

            return node;
        }
        #endregion
        #region AddItem
        public static void AddItem(this Tree tree, double value)
        {
            if (tree.Root == null) tree.Root = new Node(value);
            else
                InsertNewNode(tree.Root, value);

            tree.Count++;
        }

        private static void InsertNewNode(Node node, double value)
        {
            if (node.Value < value)
            {
                if (node.Right == null)
                {
                    node.Right = new Node(value);
                }
                else
                {
                    InsertNewNode(node.Right, value);
                }
            }
            else
            {
                if (node.Left == null)
                {
                    node.Left = new Node(value);
                }
                else
                {
                    InsertNewNode(node.Left, value);
                }
            }
        }
        #endregion
        #region Contain
        public static bool Contains(this Tree tree, double value)
        {
            // Поиск узла осуществляется другим методом.
            return FindWithParent(tree, value, out Node parent) != null;
        }

        /// 
        /// Находит и возвращает первый узел с заданным значением. Если значение
        /// не найдено, возвращает null. Также возвращает родителя найденного узла (или null)
        /// для использования в методе Remove.
        /// 
        private static Node FindWithParent(Tree tree, double value, out Node parent)
        {
            // Попробуем найти значение в дереве.
            Node current = tree.Root;
            parent = null;

            // До тех пор, пока не нашли...
            while (current != null)
            {
                int result = current.CompareTo(value);

                if (result > 0)
                {
                    // Если искомое значение меньше, идем налево.
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // Если искомое значение больше, идем направо.
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    // Если равны, то останавливаемся
                    break;
                }
            }

            return current;
        }
        #endregion
        public static bool Remove(this Tree tree, double value)
        {
            Node current, parent;

            // Находим удаляемый узел.
            current = FindWithParent(tree, value, out parent);

            if (current == null)
            {
                return false;
            }

            if (current.Right == null) //немає правого піддерева
            {
                if (parent == null) { tree.Root = current.Left; }
                else
                {
                    int result = parent.CompareTo(current.Value);//дивимося чи елемент справа від кореня чи зліва

                    //елемент злів
                    if (result > 0) { parent.Left = current.Left; }
                    //елемент справа
                    else if (result < 0) { parent.Right = current.Left; }

                }
            }
            else if (current.Left == null) //немає лівого піддерева
            {
                if (parent == null) { tree.Root = current.Right; }
                else
                {
                    int result = parent.CompareTo(current.Value);//дивимося чи елемент справа від кореня чи зліва

                    //елемент злів
                    if (result > 0) { parent.Left = current.Right; }
                    //елемент справа
                    else if (result < 0) { parent.Right = current.Right; }
                }
            }
            else if (current.Left == null && current.Right == null)
            {
                int result = parent.CompareTo(current.Value);//дивимося чи елемент справа від кореня чи зліва

                //елемент злів
                if (result > 0) { parent.Left = null; }
                //елемент справа
                else if (result < 0) { parent.Right = null; }
            }
            else
            {
                Node par = current;

                var temp = FindWithouLeft(tree, current.Right, ref par) ;

                if (parent == null)
                {
                    if (par != current)
                    {
                        tree.Root = temp;
                        par.Left = temp.Right;
                        tree.Root.Left = current.Left;
                        tree.Root.Right = current.Right;
                    }
                    else
                    {
                        tree.Root = temp;
                        temp.Left = current.Left;
                    }
                }
                else
                {
                    int result = parent.CompareTo(current.Value);//дивимося чи елемент справа від кореня чи зліва

                    //елемент злів
                    if (result > 0) { parent.Left = temp; parent.Left.Left = current.Left; par.Left = (temp.Right != null) ? temp.Right : null; }
                    //елемент справа
                    else if (result < 0) { parent.Right = temp; parent.Right.Left = current.Left; parent.Right.Right = current.Right; }
                }
            }
                return true;
        }

        private static Node FindWithouLeft(Tree tree, Node start, ref Node par)
        {
            Node temp = start;
            var next = true;

            while(next)
            {
                if (temp.Left == null) next = false;
                else
                {
                    par = temp;
                    temp = temp.Left;
                }
            }

            return temp;
        }

        public static void Sort(this double[] arr)
        {
            HeapSort.HeapSortDo(arr, arr.Length);
        }

        public static double GetMin(this Tree tree)
        {
            Node val = tree.Root;
            return FindMinimum(ref val);
        }

        private static double FindMinimum(ref Node val)
        {
            var temp = val.Left;
            if (temp != null) { return FindMinimum(ref temp); }
            else return val.Value;
        }

        public static double GetMax(this Tree tree)
        {
            Node val = tree.Root;
            return FindMaximum(ref val);
        }

        private static double FindMaximum(ref Node val)
        {
            var temp = val.Right;
            if (temp != null) { return FindMaximum(ref temp); }
            else return val.Value;
        }
    }
}
