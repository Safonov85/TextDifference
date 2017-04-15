using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextDifference
{
    public struct LongestCommonSubstringResult
    {
        private readonly bool _Success;
        private readonly int _PositionInFirstCollection;
        private readonly int _PositionInSecondCollection;
        private readonly int _Length;

        public LongestCommonSubstringResult(
            int positionInFirstCollection,
            int positionInSecondCollection,
            int length)
        {
            _Success = true;
            _PositionInFirstCollection = positionInFirstCollection;
            _PositionInSecondCollection = positionInSecondCollection;
            _Length = length;
        }

        public bool Success
        {
            get
            {
                return _Success;
            }
        }

        public int PositionInFirstCollection
        {
            get
            {
                return _PositionInFirstCollection;
            }
        }

        public int PositionInSecondCollection
        {
            get
            {
                return _PositionInSecondCollection;
            }
        }

        public int Length
        {
            get
            {
                return _Length;
            }
        }

        public override string ToString()
        {
            if (Success)
                return string.Format(
                    "LCS ({0}, {1} x{2})",
                    PositionInFirstCollection,
                    PositionInSecondCollection,
                    Length);
            else
                return "LCS (-)";
        }
    }
}

