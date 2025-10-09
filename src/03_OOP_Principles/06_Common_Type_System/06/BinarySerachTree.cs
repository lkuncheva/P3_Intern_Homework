namespace _06;

public class BinarySearchTree<T> : ICloneable where T : IComparable<T>
{
    private Node<T>? root;

    public void Add(T element)
    {
        root = AddRecursive(root, element);
    }

    private Node<T> AddRecursive(Node<T>? current, T element)
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

    private bool SearchRecursive(Node<T>? current, T element)
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

    private Node<T>? DeleteRecursive(Node<T>? root, T element)
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

    private void InOrderTraversal(Node<T>? node, List<T> result)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left, result);
            result.Add(node.Data);
            InOrderTraversal(node.Right, result);
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not BinarySearchTree<T> other)
        {
            return false;
        }

        return AreStructurallyEqual(this.root, other.root);
    }

    private bool AreStructurallyEqual(Node<T>? node1, Node<T>? node2)
    {
        if (node1 == null && node2 == null)
        {
            return true;
        }

        if (node1 == null || node2 == null)
        {
            return false;
        }

        return node1.Data.CompareTo(node2.Data) == 0 &&
               AreStructurallyEqual(node1.Left, node2.Left) &&
               AreStructurallyEqual(node1.Right, node2.Right);
    }

    public override int GetHashCode()
    {
        return GetHashCodeRecursive(root);
    }

    private int GetHashCodeRecursive(Node<T>? node)
    {
        const int primeMultiplier = 31;
        int hash = 17;

        if (node == null) return hash;

        hash = hash * primeMultiplier + GetHashCodeRecursive(node.Left);
        hash = hash * primeMultiplier + node.Data.GetHashCode();
        hash = hash * primeMultiplier + GetHashCodeRecursive(node.Right);

        return hash;
    }

    public static bool operator ==(BinarySearchTree<T>? tree1, BinarySearchTree<T> tree2)
    {
        if (ReferenceEquals(tree1, tree2)) return true;
        if (ReferenceEquals(tree1, null) || ReferenceEquals(tree2, null)) return false;

        return tree1.Equals(tree2);
    }

    public static bool operator !=(BinarySearchTree<T>? tree1, BinarySearchTree<T> tree2)
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