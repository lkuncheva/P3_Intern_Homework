namespace _06;

public class Node<T> where T : IComparable<T>
{
    public T Data { get; set; }
    public Node<T>? Left { get; set; }
    public Node<T>? Right { get; set; }

    public Node(T data)
    {
        Data = data;
    }

    public Node<T> DeepCopy()
    {
        Node<T> newRoot = new Node<T>(this.Data);

        if (this.Left != null)
        {
            newRoot.Left = this.Left.DeepCopy();
        }

        if (this.Right != null)
        {
            newRoot.Right = this.Right.DeepCopy();
        }

        return newRoot;
    }
}