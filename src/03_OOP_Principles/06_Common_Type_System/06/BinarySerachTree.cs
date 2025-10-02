namespace _06;

public class BinarySearchTree<T> : ICloneable where T : IComparable<T>
{
    private Node<T> root;

    public void Add(T element)
    {
        root = AddRecursive(root, element);
    }

    private Node<T> AddRecursive(Node<T> current, T element)
    {
        if (current == null) return new Node<T>(element);

        if (element.CompareTo(current.Data) < 0)
        {
            current.Left = AddRecursive(current.Left, element);
        }
        else if (element.CompareTo(current.Data) > 0)
        {
            current.Right = AddRecursive(current.Right, element);
        }

        return current;
    }

    public bool Search(T element)
    {
        return SearchRecursive(root, element);
    }

    private bool SearchRecursive(Node<T> current, T element)
    {
        if (current == null) return false;
        if (element.CompareTo(current.Data) == 0) return true;

        if (element.CompareTo(current.Data) < 0)
        {
            return SearchRecursive(current.Left, element);
        }
        else
        {
            return SearchRecursive(current.Right, element);
        }
    }

    public void Delete(T element)
    {
        root = DeleteRecursive(root, element);
    }

    private Node<T> DeleteRecursive(Node<T> root, T element)
    {
        if (root == null) return root;

        if (element.CompareTo(root.Data) < 0)
        {
            root.Left = DeleteRecursive(root.Left, element);
        }
        else if (element.CompareTo(root.Data) > 0)
        {
            root.Right = DeleteRecursive(root.Right, element);
        }
        else
        {
            if (root.Left == null) return root.Right;
            if (root.Right == null) return root.Left;

            root.Data = MinValue(root.Right);

            root.Right = DeleteRecursive(root.Right, root.Data);
        }

        return root;
    }

    private T MinValue(Node<T> node)
    {
        T minv = node.Data;

        while (node.Left != null)
        {
            minv = node.Left.Data;
            node = node.Left;
        }

        return minv;
    }

    public override string ToString()
    {
        var result = new List<T>();
        InOrderTraversal(root, result);

        return $"[ {string.Join(", ", result)} ]";
    }

    private void InOrderTraversal(Node<T> node, List<T> result)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left, result);
            result.Add(node.Data);
            InOrderTraversal(node.Right, result);
        }
    }

    public override bool Equals(object obj)
    {
        return obj is BinarySearchTree<T> other && this.ToString() == other.ToString();
    }

    public override int GetHashCode()
    {
        return root != null ? root.Data.GetHashCode() : 0;
    }

    public static bool operator ==(BinarySearchTree<T> tree1, BinarySearchTree<T> tree2)
    {
        if (ReferenceEquals(tree1, tree2)) return true;
        if (ReferenceEquals(tree1, null) || ReferenceEquals(tree2, null)) return false;

        return tree1.Equals(tree2);
    }

    public static bool operator !=(BinarySearchTree<T> tree1, BinarySearchTree<T> tree2)
    {
        return !(tree1 == tree2);
    }

    public object Clone()
    {
        var clone = new BinarySearchTree<T>();
        if (root != null)
        {
            clone.root = this.root.DeepCopy();
        }

        return clone;
    }
}