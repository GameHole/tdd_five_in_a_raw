using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public struct Result
    {
        public int code { get; private set; }
        public Result(int c)
        {
            code = c; 
        }
        public static bool operator == (Result a,Result b)
        {
            return a.code == b.code;
        }
        public static bool operator !=(Result a, Result b)
        {
            return !(a==b);
        }
        public override bool Equals(object obj)
        {
            if (obj is Result result)
                return result.code == code;
            return false;
        }
        public override int GetHashCode()
        {
            return code;
        }
        public override string ToString()
        {
            return $"code:{code}";
        }
    }
}
