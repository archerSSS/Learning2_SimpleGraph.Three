using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class Vertex<T>
    {
        public bool Hit;
        public T Value;
        public Vertex(T val)
        {
            Value = val;
            Hit = false;
        }
    }

    public class SimpleGraph<T>
    {
        public Queue<Trace<Vertex<T>>> queue;
        public Stack<Vertex<T>> stack;
        public Vertex<T>[] vertex;
        public int[,] m_adjacency;
        public int max_vertex;

        public SimpleGraph(int size)
        {
            max_vertex = size;
            m_adjacency = new int[size, size];
            vertex = new Vertex<T>[size];
            queue = new Queue<Trace<Vertex<T>>>();
            stack = new Stack<Vertex<T>>();
        }

        public void AddVertex(T value)
        {
            for (int i = 0; i < max_vertex; i++)
                if (vertex[i] == null)
                {
                    vertex[i] = new Vertex<T>(value);
                    return;
                }
        }

        public void RemoveVertex(int v)
        {
            if (IsInRange(v) && vertex[v] != null)
            {
                for (int i = 0; i < max_vertex; i++)
                {
                    m_adjacency[v, i] = 0;
                    m_adjacency[i, v] = 0;
                }
                vertex[v] = null;
            }
        }

        public bool IsEdge(int v1, int v2)
        {
            if (IsInRange(v1, v2)) return m_adjacency[v1, v2] == 1 && m_adjacency[v2, v1] == 1;
            return false;
        }

        public void AddEdge(int v1, int v2)
        {
            if (IsInRange(v1, v2) && vertex[v1] != null && vertex[v2] != null)
            {
                m_adjacency[v1, v2] = 1;
                m_adjacency[v2, v1] = 1;
            }
        }

        public void RemoveEdge(int v1, int v2)
        {
            if (IsInRange(v1, v2) && vertex[v1] != null && vertex[v2] != null)
            {
                m_adjacency[v1, v2] = 0;
                m_adjacency[v2, v1] = 0;
            }
        }

        public List<Vertex<T>> DepthFirstSearch(int VFrom, int VTo)
        {
            ClearStackData();
            List<Vertex<T>> list = new List<Vertex<T>>();
            for (int i = 0; i < max_vertex; i++) vertex[i].Hit = false;
            if (NextStackVert(VFrom, VTo)) stack.Push(vertex[VTo]);
            return StackToList(list);
        }

        public List<Vertex<T>> BreadthFirstSearch(int VFrom, int VTo)
        {
            return TracesToList(NextQueueVert(LeaveVertexTrace(VFrom), VTo));
        }

        private bool IsInRange(int v)
        {
            return v < max_vertex;
        }

        private bool IsInRange(int a, int b)
        {
            return a < max_vertex && b < max_vertex;
        }

        private bool HitVertex(Vertex<T> vertex)
        {
            return vertex.Hit = true;
        }

        private bool IsNotEmpty(int index)
        {
            return index < max_vertex && vertex[index] != null;
        }

        private Vertex<T> GetVertex(int index)
        {
            if (IsNotEmpty(index)) return vertex[index];
            return null;
        }

        private Trace<Vertex<T>> LeaveVertexTrace(int index)
        {
            if (IsNotEmpty(index)) return new Trace<Vertex<T>>(index);
            return null;
        }

        private void ClearStackData()
        {
            stack = new Stack<Vertex<T>>();
            for (int i = 0; i < max_vertex; i++) vertex[i].Hit = false;
        }

        // -- Добавляет указанную вершину в стак.
        // -- Проводит проверку внутри цикла Cyc.
        //
        private bool NextStackVert(int VPres, int VTo)
        {
            if (AVS(VPres) && Cyc(0, VPres, VTo)) return true;
            return RVS();
        }

        // Копирует вершины из указанного Стака в стандартный Список
        //
        private List<Vertex<T>> StackToList(List<Vertex<T>> list)
        {
            for (int i = stack.dyn.count - 1; i > -1; i--) list.Add(stack.dyn.array[i]);
            return list;
        }

        // -- Альтернатива циклу for.
        // -- Также проверяет на наличие ребра у указанных вершин - ожидается true
        //          был ли проведен обход по указанной вершине - ожидается false
        //              и является эта вершина искомой.
        // -- Если все условия кроме последнего удовлетворены, то проводится проверка следующей выбранной вершины.
        //
        private bool Cyc(int i, int VPres, int VTo)
        {
            if (IsOver(i)) return false;
            else if (!vertex[i].Hit && IsEdge(VPres, i) && (i == VTo || NextStackVert(i, VTo))) return true;
            return Cyc(i + 1, VPres, VTo);
        }

        // Add Vertix to Stack
        //
        private bool AVS(int i)
        {
            vertex[i].Hit = true;
            stack.Push(vertex[i]);
            return true;
        }

        // Remove Vertex from Stack
        //
        private bool RVS()
        {
            stack.Pop();
            return false;
        }

        // Проводит проверку на завершение цикла
        //
        private bool IsOver(int i)
        {
            return i >= max_vertex;
        }

        private Trace<Vertex<T>> NextQueueVert(Trace<Vertex<T>> step, int VTo)
        {
            if (step == null) return null; 
            for (int i = 0; i < max_vertex; i++)
                if (vertex[i] != null && !vertex[i].Hit && IsEdge(step.num, i))
                {
                    if (i == VTo) return step.SetNextTrace(step, i);
                    vertex[i].Hit = true;
                    queue.Enqueue(step.SetNextTrace(step, i));
                }
            return NextQueueVert(queue.Dequeue(), VTo);
        }

        private Trace<Vertex<T>> ThrowCroubs(Trace<Vertex<T>> trace)
        {
            while (trace.steprev != null)
            {
                trace.steprev.nextep = trace;
                trace = trace.steprev;
            }
            return trace;
        }
        
        private List<Vertex<T>> TracesToList(Trace<Vertex<T>> trace)
        {
            List<Vertex<T>> list = new List<Vertex<T>>();
            if (trace != null)
            {
                trace = ThrowCroubs(trace);
                while (trace != null)
                {
                    list.Add(vertex[trace.num]);
                    trace = trace.nextep;
                }
            }
            return list;
        }
    }

    public class Stack<T>
    {

        public DynArray<T> dyn;

        public Stack()
        {
            dyn = new DynArray<T>();
        }

        public int Size()
        {
            if (dyn.count != 0) return dyn.count;
            return 0;
        }

        public T Pop()
        {
            if (dyn.count > 0)
            {
                T val = dyn.GetItem(0);
                dyn.Remove(0);
                if (dyn.count == 0) return val;
                return val;
            }
            return default(T);
        }

        public void Push(T val)
        {
            if (val == null) return;
            dyn.Insert(val, 0);
        }

        public T Peek()
        {
            if (dyn.count != 0)
            {
                return dyn.GetItem(0);
            }
            return default(T);
        }
    }

    public class DynArray<T>
    {

        public T[] array;
        public int count;
        public int capacity;

        public DynArray()
        {
            count = 0;
            MakeArray(16);
        }


        public void MakeArray(int new_capacity)
        {
            if (array != null)
            {
                if (new_capacity < 16) new_capacity = 16;
                capacity = new_capacity;
            }
            else
            {
                array = new T[count];
                capacity = new_capacity;
            }
        }


        public T GetItem(int index)
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException();
            return array[index];
        }


        public void Append(T itm)
        {
            T[] temp_array = new T[count + 1];
            array.CopyTo(temp_array, 0);
            temp_array[count] = itm;
            array = temp_array;
            count++;
            if (count > capacity) MakeArray(capacity * 2);
        }


        public void Insert(T itm, int index)
        {
            if (index < 0 || index > count) throw new IndexOutOfRangeException();
            if (index == count) Append(itm);
            else
            {
                T[] temp_array = new T[count + 1];
                array.CopyTo(temp_array, 0);
                for (int i = count; i > index; i--)
                {
                    temp_array[i] = array[i - 1];
                }
                temp_array[index] = itm;
                array = temp_array;
                count++;
                if (count > capacity) MakeArray(capacity * 2);
            }
        }


        public void Remove(int index)
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException();
            T[] temp_array = new T[count - 1];
            for (int i = count - 2; i >= 0; i--)
            {
                if (i >= index) temp_array[i] = array[i + 1];
                else temp_array[i] = array[i];
            }
            array = temp_array;
            count--;
            if (count < capacity / 2) { MakeArray((int)(capacity / 1.5)); }
        }
    }

    public class Queue<T>
    {

        List<T> list;

        public Queue()
        {
            list = new List<T>();
        }

        public void Enqueue(T item)
        {
            list.Add(item);
        }

        public T Dequeue()
        {
            if (list.Count != 0)
            {
                T item = list.Find(delegate (T it) { return it.GetType() == typeof(T); });
                list.RemoveAt(0);
                return item;
            }
            return default(T);
        }

        public int Size()
        {
            return list.Count;
            return 0;
        }

        public void HeadToTail(int count)
        {
            if (list.Count == 0) return;
            for (int i = 0; i < count; i++)
            {
                Enqueue(Dequeue());
            }
        }
    }

    
    public class Trace<T>
    {
        public int num;
        public Trace<T> nextep;
        public Trace<T> steprev;

        public Trace(int n)
        {
            num = n;
        }
        
        public Trace<T> SetNextTrace(Trace<T> step, int n)
        {
            Trace<T> nextep = new Trace<T>(n);
            nextep.steprev = step;
            return nextep;
        }
    }
}