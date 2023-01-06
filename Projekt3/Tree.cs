using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projekt3
{
    public class Tree
    {
        public static int InsertMoveCount = 0;
        public static int SearchMoveCount = 0;
        public static int DeleteMoveCount = 0;

        public Node root;
        public int depth
        {
            get
            {
                return GetDepth(root);
            }
            set
            {
                depth = value;
            }
        }
        public Tree()
        {
            root = null;
        }
        public Tree(string value)
        {
            root = new Node(value);
        }

        #region insert
        public void Insert(string value, bool shouldCount)
        {
            root = InsertRec(root, value, shouldCount);
        }

        public Node InsertRec(Node actualRoot, string value, bool shouldCount)
        {
            if (shouldCount == false)
            {
                InsertMoveCount = 0;
            }

            if (actualRoot == null)
            {
                if (shouldCount)
                {
                    InsertMoveCount++;
                }

                actualRoot = new Node(value);
                return actualRoot;
            }

            if (string.Compare(actualRoot.value, value) > 0)
            {
                if (shouldCount)
                {
                    InsertMoveCount++;
                }

                actualRoot.left = InsertRec(actualRoot.left, value, shouldCount);
            }
            else if (string.Compare(actualRoot.value, value) <= 0)
            {
                if (shouldCount)
                {
                    InsertMoveCount++;
                }

                actualRoot.right = InsertRec(actualRoot.right, value, shouldCount);
            }

            return actualRoot;
        }
        #endregion insert

        #region search
        public Node Search(string value, bool shouldCount)
        {
            return SearchRec(root, value, shouldCount);
        }

        public Node SearchRec(Node actualRoot, string value, bool shouldCount)
        {
            if (actualRoot == null || actualRoot.value == value)
            {
                if (shouldCount)
                {
                    SearchMoveCount++;
                }

                return actualRoot;
            }
            if (string.Compare(actualRoot.value, value) < 0)
            {
                if (shouldCount)
                {
                    SearchMoveCount++;
                }

                return SearchRec(actualRoot.right, value, shouldCount);
            }
            else
            {
                if (shouldCount)
                {
                    SearchMoveCount++;
                }

                return SearchRec(actualRoot.left, value, shouldCount);
            }
        }
        #endregion search

        #region delete
        public void Delete(string value, bool shouldCount)
        {
            root = DeleteRec(root, value, shouldCount);
        }

        public Node DeleteRec(Node actualRoot, string value, bool shouldCount)
        {
            if (actualRoot == null)
            {
                return actualRoot;
            }

            if (string.Compare(actualRoot.value, value) > 0)
            {
                if (shouldCount)
                {
                    DeleteMoveCount++;
                }
                actualRoot.left = DeleteRec(actualRoot.left, value, shouldCount);
            }
            else if (string.Compare(actualRoot.value, value) < 0)
            {
                if (shouldCount)
                {
                    DeleteMoveCount++;
                }
                actualRoot.right = DeleteRec(actualRoot.right, value, shouldCount);
            }
            else
            {
                if (actualRoot.left == null)
                {
                    return actualRoot.right;
                }
                else if (actualRoot.right == null)
                {
                    return actualRoot.left;
                }
                else
                {
                    actualRoot.value = minValue(actualRoot.right);

                    if (shouldCount)
                    {
                        DeleteMoveCount++;
                    }
                    actualRoot.right = DeleteRec(actualRoot.right, actualRoot.value, shouldCount);
                }
            }
            return actualRoot;
        }

        string minValue(Node actualRoot)
        {
            string minv = actualRoot.value;
            while (actualRoot.left != null)
            {
                minv = actualRoot.left.value;
                actualRoot = actualRoot.left;
            }
            return minv;
        }


        #endregion delete
        #region writeOut
        public void Print()
        {
            inorder();
            Console.WriteLine();
        }

        void inorder()
        {
            inorderRec(root);
        }

        void inorderRec(Node root)
        {
            if (root != null)
            {
                inorderRec(root.left);
                Console.Write(root.value + " ");
                inorderRec(root.right);
            }
        }

        #endregion writeOut

        int GetDepth(Node actualRoot)
        {
            if (actualRoot == null)
            {
                return 0;
            }

            int leftDepth = GetDepth(actualRoot.left);
            int rightDepth = GetDepth(actualRoot.right);

            return Math.Max(leftDepth, rightDepth) + 1;
        }

        public static void ResetData()
        {
            InsertMoveCount = 0;
            SearchMoveCount = 0;
            DeleteMoveCount = 0;
        }
    }

    public class Node
    {
        public string value;
        public Node left = null;
        public Node right = null;

        public Node()
        {
            value = "";
        }

        public Node(string value)
        {
            this.value = value;
        }
    }
}

