
using System.Buffers;

namespace RevStar.Foundation;

/// <summary>
/// A collection of memory-related utility functions 
/// for efficient handling of arrays, buffers, and spans.
/// </summary>
public static class MemoryUtils
{
    /// <summary>
    /// Splits a large array into smaller sub-spans of specified length.
    /// This can avoid copying large sections of memory unnecessarily.
    /// </summary>
    /// <param name="source">The original array.</param>
    /// <param name="chunkSize">The size of each chunk.</param>
    /// <returns>An enumerable of Span segments.</returns>
    public static IEnumerable<ReadOnlyMemory<T>> SplitIntoSpans<T>(T[] source, int chunkSize)
    {
        for (int i = 0; i < source.Length; i += chunkSize)
        {
            int remaining = Math.Min(chunkSize, source.Length - i);
            yield return new ReadOnlyMemory<T>(source, i, remaining);
        }
    }

    /// <summary>
    /// Example method: returns a rented buffer from the ArrayPool,
    /// which is then more safely returned after usage.
    /// </summary>
    /// <param name="size">Number of elements in the buffer.</param>
    /// <returns>A rented array from the shared ArrayPool.</returns>
    public static T[] RentBuffer<T>(int size)
    {
        return ArrayPool<T>.Shared.Rent(size);
    }

    /// <summary>
    /// Example method: returns a rented buffer to the pool.
    /// </summary>
    public static void ReturnBuffer<T>(T[] buffer)
    {
        ArrayPool<T>.Shared.Return(buffer, clearArray: true);
    }
}
