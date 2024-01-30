using System;
using System.Text;

namespace lab2
{
    public class TextModifier
    {

        private static readonly List<string> s_consonantLetters =
            new List<string>() {"B", "C", "D", "F", "G", "H", "J", "K", "L", "M",
    "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z"};


        private readonly StringBuilder _sb;
        private readonly int _removeLength;
        private int _startSpaceIdx = -1;
		private int _nextSpaceIdx = 0;
        private string _substr;

        private bool SubstringContainsNonChars
        {
            get => _substr.Any(c => c.ToString() is "." or "," or "!");
        }

        private bool SubstringStartsWithConsonant
        {
            get => s_consonantLetters.Contains(_substr.First().ToString().ToUpper());
        }

        private bool SubstringMatches
        {
            get => _substr.Length == _removeLength &&
                !SubstringContainsNonChars &&
                SubstringStartsWithConsonant;
        }



        public TextModifier(string text, int removeLength)
		{
			_sb = new StringBuilder(text);
			_removeLength = removeLength;
		}

        public void RemoveWords()
        {
            while (_nextSpaceIdx != -1)
            {
                _nextSpaceIdx = _sb.IndexOf(" ", _startSpaceIdx + 1);
                if (_nextSpaceIdx == -1 && _startSpaceIdx == -1)
                {
                    HandleOneWordCase();
                    break;
                }
                if (_nextSpaceIdx != -1)
                {
                    _substr = _sb.Substring(_startSpaceIdx + 1, _nextSpaceIdx);
                } else
                {
                    _substr = _sb.Substring(_startSpaceIdx + 1);
                }
                if (SubstringMatches)
                {
                    _startSpaceIdx = GetStartSpaceIdxForFoundMatch();
                } else
                {
                    if (SubstringContainsNonChars)
                    {
                        _startSpaceIdx = GetNextIdxForNonChar();
                    } else
                    {
                        _startSpaceIdx = _nextSpaceIdx;
                    }
                }
            }
            Console.WriteLine($"final string: {_sb}");
        }

        private void HandleOneWordCase()
        {
            if (_sb.Length == _removeLength)
            {
                _sb.Replace(_sb.ToString(), "");
            }

        }

        private int GetStartSpaceIdxForFoundMatch(int endOffset = 0)
        {
            int endIdx;
            if (_nextSpaceIdx == -1)
            {
                endIdx = _sb.Length;
            } else
            {
                endIdx = _nextSpaceIdx - endOffset;
            }
            _sb.Replace(_startSpaceIdx + 1, endIdx, "");
            return _sb.IndexOf(" ", _startSpaceIdx + 1);
        }

        private int GetNextIdxForNonChar()
        {
            var endOffset = 0;
            while (SubstringContainsNonChars)
            {
                _substr = _substr.Substring(0, _substr.Length - 1);
                endOffset++;
            }
            if (_substr.Length == _removeLength && SubstringStartsWithConsonant)
            {
                return GetStartSpaceIdxForFoundMatch(endOffset);
            }
            return _nextSpaceIdx;
        }




    }
}

