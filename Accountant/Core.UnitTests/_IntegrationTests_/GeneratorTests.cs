using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAssertions;

using NUnit.Framework;

namespace NewModel.UnitTests._IntegrationTests_
{
    [TestFixture]
    public sealed class GeneratorTests
    {
        #region ' Direct '

        [Test]
        [TestCase("X", "b", "a", "b")]
        [TestCase("a", "b", "a", "b")]
        [TestCase("a", "b", "b", null)]
        [TestCase("X", "X", "whatever", "whatever")]
        [TestCase("X", "Y", null)]
        [TestCase("a", "X", null)]
        [TestCase("X", "aX", "rea", "area")]
        [TestCase("aX", "X", "area", "rea")]
        [TestCase("aX", "X", "wrong", null)]
        [TestCase("Xa", "X", "rea", "re")]
        [TestCase("Xa", "X", "wrong", null)]
        [TestCase("X", "XX", "done", "donedone")]
        [TestCase("X", "XX", "", null)]
        [TestCase("XX", "X", "dodo", "do")]
        [TestCase("XX", "X", "odd", null)]
        [TestCase("XX", "X", "done", null)]
        [TestCase("XX", "X", "", null)]
        public void TestDirect(string argument, string result, string value, string expectedResult)
        {
            var func = Direct(argument, result);
            if (value == null) func.Should().BeNull();
            else func(value).Should().Be(expectedResult);
        }

        static Func<string, string> Direct(string argument, string result)
        {
            var a = Parse(argument);
            var r = ParseToTheLetter(result);
            return value =>
            {
                var input = Resolve(a, value);
                if (input == null) return null;
                var normalized = NormalizeList(input);
                if (normalized == null) return null;
                return string.Join("", r.Select(o => IsVariable(o) ? normalized[o] : o));
            };
        }

        #endregion

        #region ' Parse '

        [Test]
        [TestCase("A", new[] { "A" })]
        [TestCase("a", new[] { "a" })]
        [TestCase("AB", new[] { "AB" })]
        [TestCase("ab", new[] { "ab" })]
        [TestCase("abX", new[] { "ab", "X" })]
        [TestCase("Xab", new[] { "X", "ab" })]
        [TestCase("", new string[0])]
        [TestCase("aXXabX", new[] { "a", "XX", "ab", "X" })]
        public void TestParse(string input, string[] expectedOutput)
        {
            Parse(input).Should().Equal(expectedOutput);
        }
        enum ParseState { None, Variable, Constant }
        static IEnumerable<string> Parse(string s)
        {
            var sb = new StringBuilder();
            var isConstant = ParseState.None;
            foreach (var c in s)
            {
                if (char.IsUpper(c))
                {
                    if (isConstant == ParseState.Constant)
                    {
                        yield return sb.ToString();
                        sb.Clear();
                    }
                    sb.Append(c);
                    isConstant = ParseState.Variable;
                }
                else
                {
                    if (isConstant == ParseState.Variable)
                    {
                        yield return sb.ToString();
                        sb.Clear();
                    }
                    sb.Append(c);
                    isConstant = ParseState.Constant;
                }
            }
            if (sb.Length != 0) yield return sb.ToString();
        }

        #endregion

        #region ' Resolve '

        static Dictionary<string, string> Resolve(IEnumerable<string> arguments, string value)
        {
            var result = new Dictionary<string, string>();
            var currentVariable = default(string);
            var reader = new MyStringReader(value);
            var argumentsBuffered = arguments.ToList();
            reader.TailLength = argumentsBuffered.Sum(a => a.Length);
            foreach (var argument in argumentsBuffered)
            {
                reader.TailLength -= argument.Length;
                if (IsConstant(argument))
                {
                    if (currentVariable != null)
                    {
                        var variable = reader.ReadVariable(currentVariable.Length, argument);
                        if (!Put(result, currentVariable, variable)) return null;
                        currentVariable = null;
                    }
                    if (!reader.ReadConstant(argument)) return null;
                }
                else //if (IsVariable(argument))
                {
                    currentVariable = argument;
                }
            }
            if (currentVariable != null)
            {
                var variable = reader.ReadToTheEnd(currentVariable.Length);
                if (variable == null) return null;
                if (!Put(result, currentVariable, variable)) return null;
            }
            return result;
        }

        static bool Put(Dictionary<string, string> result, string variable, string value)
        {
            string previous;
            if (result.TryGetValue(variable, out previous))
                if (previous != value)
                    return false;
            result[variable] = value;
            return true;
        }

        [Test]
        [TestCase("const", "const", "")]
        [TestCase("const", "wrong", null)]
        [TestCase("X", "value", "X = value")]
        [TestCase("XX", "value", "XX = value")]
        [TestCase("constX", "constant", "X = ant")]
        [TestCase("Xant", "constant", "X = const")]
        [TestCase("XantY", "constant", null)]
        [TestCase("XaY", "constant", "X = const; Y = nt")]
        [TestCase("XaY", ".aa.", null)]
        [TestCase("XaY", "..aa", "X = ..; Y = a")]
        [TestCase("X.X", "a.a", "X = a")]
        [TestCase("XY.XY", "ab.ab", "XY = ab")]
        [TestCase("X.X", "a.b", null)]
        public void TestResolve(string argument, string value, string expectedResult)
        {
            var args = Parse(argument).ToList();
            var result = Resolve(args, value);
            if (expectedResult == null) result.Should().BeNull();
            else
            {
                var dictionary = expectedResult.Replace(" ", "")
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToDictionary(s => s.Split('=')[0], s => s.Split('=')[1]);
                result.Should().Equal(dictionary);
            }
        }

        #endregion

        #region ' Normalize '

        private static Dictionary<string, string> Normalize(string key, string value)
        {
            var result = new Dictionary<string, string>();
            if (key.Length == 1)
            {
                result[key] = value;
                return result;
            }
            if (key.Length == value.Length)
            {
                for (var i = 0; i < key.Length; i++)
                {
                    string v;
                    var k = key.Substring(i, 1);
                    if (result.TryGetValue(k, out v))
                    {
                        if (v != value.Substring(i, 1)) return null;
                    }
                    else result[k] = value.Substring(i, 1);
                }
                return result;
            }
            if (key.Length > 1)
            {
                if (value.Length % key.Length != 0) return null;
                var partSize = value.Length / key.Length;
                var part = value.Substring(0, partSize);
                var allEqual = Enumerable.Range(1, key.Length - 1).All(i =>
                    string.CompareOrdinal(value, i * partSize, part, 0, partSize) == 0);
                if (!allEqual) return null;
                result[key.Substring(0, 1)] = part;
                return result;
            }
            return null;
        }

        [Test]
        public void Normalize_Should_Skip_Single_Letter_Variable()
        {
            Normalize("X", "value").Should().Equal(
                new Dictionary<string, string> { { "X", "value" } });
        }

        [Test]
        public void Normalize_Should_Split_Repeaters()
        {
            Normalize("XX", "mama").Should().Equal(
                new Dictionary<string, string> { { "X", "ma" } });
        }
        [Test]
        public void Normalize_When_Not_Dividable_Length_Should_Return_Null()
        {
            Normalize("XX", "mam").Should().BeNull();
        }
        [Test]
        public void Normalize_When_Not_Equal_Parts_Should_Return_Null()
        {
            Normalize("XX", "mapa").Should().BeNull();
        }
        [Test]
        public void Normalize_When_Key_Length_Equals_Value_Length_Should_Split_By_Letter()
        {
            Normalize("XY", "ma").Should().Equal(
                new Dictionary<string, string> { { "X", "m" }, { "Y", "a" } });
        }
        [Test]
        public void Normalize_When_Key_Occurs_Twice_Should_Return_It_Once()
        {
            Normalize("XYY", "maa").Should().Equal(
                new Dictionary<string, string> { { "X", "m" }, { "Y", "a" } });
        }
        [Test]
        public void Normalize_When_Key_Occurs_Twice_And_DoesNot_Match_Should_Return_Null()
        {
            Normalize("XYY", "mab").Should().BeNull();
        }

        #endregion

        #region ' NormalizeList '

        private static Dictionary<string, string> NormalizeList(Dictionary<string, string> input)
        {
            var result = new Dictionary<string, string>();
            foreach (var pair in input)
            {
                var subPairs = Normalize(pair.Key, pair.Value);
                if (subPairs == null) return null;
                if (subPairs.Any(subPair => !Put(result, subPair.Key, subPair.Value)))
                    return null;
            }
            return result;
        }

        [Test]
        public void NormalizeList_Should_Not_Touch_Single_Letter_Variables()
        {
            NormalizeList(new Dictionary<string, string> { { "X", "ma" } })
                .Should().Equal(new Dictionary<string, string> { { "X", "ma" } });
        }
        [Test]
        public void NormalizeList_Should_Normalize_Complex_Variables()
        {
            NormalizeList(new Dictionary<string, string> { { "XX", "mama" } })
                .Should().Equal(new Dictionary<string, string> { { "X", "ma" } });
        }
        [Test]
        public void NormalizeList_When_Values_DoNot_Match_Should_Return_Null()
        {
            NormalizeList(new Dictionary<string, string> { { "X", "pa" }, { "XX", "mama" } })
                .Should().BeNull();
        }
        [Test]
        public void NormalizeList_Normalize_Returns_Null_Should_Return_Null()
        {
            NormalizeList(new Dictionary<string, string> { { "XX", "pa" } })
                .Should().BeNull();
        }

        #endregion

        #region ' ParseToTheLetter '

        [Test]
        [TestCase("AB", new[] { "A", "B" })]
        [TestCase("ab", new[] { "ab" })]
        [TestCase("abX", new[] { "ab", "X" })]
        [TestCase("Xab", new[] { "X", "ab" })]
        [TestCase("", new string[0])]
        [TestCase("aXXabX", new[] { "a", "X", "X", "ab", "X" })]
        public void TestParseToTheLetter(string input, string[] expectedOutput)
        {
            ParseToTheLetter(input).Should().Equal(expectedOutput);
        }

        static IEnumerable<string> ParseToTheLetter(string s)
        {
            var sb = new StringBuilder();
            foreach (var c in s)
            {
                if (char.IsUpper(c))
                {
                    if (sb.Length != 0)
                    {
                        yield return sb.ToString();
                        sb.Clear();
                    }
                    yield return c.ToString();
                }
                else sb.Append(c);
            }
            if (sb.Length != 0) yield return sb.ToString();
        }

        #endregion

        static bool IsConstant(string argument)
        {
            return argument.ToLower() == argument;
        }

        static bool IsVariable(string argument)
        {
            return argument.ToUpper() == argument;
        }
    }
}