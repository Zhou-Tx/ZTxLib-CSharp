namespace ZTxLib.DataStruct
{
    /// <summary>
    /// 链表
    /// </summary>
    internal class LinkList
    {
        private class Node
        {
            public object Data { get; set; }
            internal Node Next { get; set; }
        }
        private readonly Node head;
        public int Length { get; private set; }

        public LinkList()
        {
            head = new Node
            {
                Data = null,
                Next = null
            };
            Length = 0;
        }
        public void Insert(object data, int pos)
        {
            if (pos <= 0) return;
            Node h = head;
            for (int i = 1; i < pos; i++)
                h = h.Next;
            Node node = new Node { Data = data };
            node.Next = h.Next;
            h.Next = node;
            Length++;
        }
        public void Set(object data, int pos)
        {
            if (pos <= 0) return;
            Node h = head;
            for (int i = 0; i < pos; i++)
                h = h.Next;
            h.Data = data;
        }
        public object Get(int pos)
        {
            if (pos <= 0) return null;
            Node h = head;
            for (int i = 0; i < pos; i++)
                h = h.Next;
            return h.Data;
        }
        public void Delete(int pos)
        {
            if (pos <= 0) return;
            Node h = head;
            for (int i = 1; i < pos; i++)
                h = h.Next;
            h.Next = h.Next.Next;
            Length--;
        }
        public void Print()
        {
            Node h = head;
            while (h.Next != null)
            {
                h = h.Next;
                System.Console.WriteLine(h.Data);
            }
        }
    }
}
