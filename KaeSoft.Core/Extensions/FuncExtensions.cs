using System;
using Andy.Lib.Classes;

namespace Andy.Lib.Extensions
{
    public static class FuncExtensions
    {
        public static Func<TArg, TResult> Memoize<TArg, TResult>(this Func<TArg, TResult> function)
        {
            var results = new SynchronizedDictionary<TArg, TResult>();

            return arg => results.GetOrAdd(arg, _ => function(arg));
        }

        //public static Func<TArg1, TArg2, TResult> Memoize<TArg1, TArg2, TResult>(Func<TArg1, TArg2, TResult> func)
        //{
        //    var cache = new SynchronizedDictionary<Tuple<TArg1, TArg2>, TResult>();
        //    return (a1, a2) =>
        //    {
        //        var key = Tuple.Create(a1, a2);
        //        return cache.GetOrAdd(key, _ => func(a1, a2));
        //    };
        //}

        //public static Func<TArg, IObservable<TResult>> AsyncMemoize<TArg, TResult>(this Func<TArg, TResult> function,
        //    IScheduler scheduler)
        //{
        //    var resultObservables = new SynchronizedDictionary<TArg, IObservable<TResult>>();
        //    return arg => resultObservables.GetOrAdd(arg, _ => Observable.Start(() => function(arg), scheduler));
        //}

        //public static Func<IObservable<T>> AsyncMemoize<T>(this Func<T> f, IScheduler scheduler)
        //{
        //    var gate = new object();
        //    IObservable<T> result = null;

        //    return () =>
        //    {
        //        if (result == null)
        //        {
        //            lock (gate)
        //            {
        //                if (result == null)
        //                {
        //                    result = Observable.Start(f, scheduler);
        //                }
        //            }
        //        }
        //        return result;
        //    };
        //}

        //public static Func<T> Memoize<T>(this Func<T> f)
        //{
        //    var gate = new object();
        //    var set = false;
        //    var result = default(T);
        //    Exception error = null;

        //    return () =>
        //    {
        //        if (!set)
        //        {
        //            lock (gate)
        //            {
        //                if (!set)
        //                {
        //                    try
        //                    {
        //                        result = f();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        error = ex;
        //                    }
        //                    set = true;
        //                }
        //            }
        //        }

        //        if (error != null)
        //            throw new Exception("Memorized function threw an exception", error);
        //        return result;
        //    };
        //}
    }
}
