using System.Collections.Generic;

namespace MinimalAF {
    public static class ContainerSliceExtensions {
        public static ArraySlice<T> Slice<T>(this IList<T> container, int start, int end) {
            return new ArraySlice<T>(container, start, end);
        }
    }


    /// <summary>
    /// Basically ArraySegment but a lot of syntactical sugar with minimal overhead.
    /// </summary>
    public readonly struct ArraySlice<T> {
        public readonly IList<T> Container;
        public readonly int Start;
        public readonly int End;

        public ArraySlice(IList<T> container, int a, int b) {
            Container = container;

            if (a < b) {
                Start = a;
                End = b;
            } else {
                Start = b;
                End = a;
            }
        }

        public static implicit operator ArraySlice<T>(List<T> list) => new ArraySlice<T>(list, 0, list.Count);
        public static implicit operator ArraySlice<T>(T[] array) => new ArraySlice<T>(array, 0, array.Length);

        public T this[int index] {
            get {
                return Container[Start + index];
            }
            set {
                Container[Start + index] = value;
            }
        }

        public ArraySlice<T> this[int start, int end] {
            get {
                if (end < 0) {
                    end = Length + 1 + end;
                }

                if (start < 0) {
                    start = Length + 1 + start;
                }

                return new ArraySlice<T>(Container, start, end);
            }
        }

        public int Length {
            get {
                return End - Start;
            }
        }

        /// <summary>
        /// A struct enumerator that shouldn't allocate anything.
        /// </summary>
        public ArraySliceIterator<T> GetEnumerator() {
            return new ArraySliceIterator<T>(this);
        }
    }

    /// <summary>
    /// This iterator gives the index as well as the value
    /// </summary>
    public struct ArraySliceIterator<T> {
        private ArraySlice<T> slice;
        private int index;

        public ArraySliceIterator(ArraySlice<T> slice) {
            this.slice = slice;
            index = -1;
        }

        public (int, T) Current => (index, slice[index]);

        public bool MoveNext() {
            index++;
            return index < slice.Length;
        }

        public void Reset() {
            index = -1;
        }
    }
}
