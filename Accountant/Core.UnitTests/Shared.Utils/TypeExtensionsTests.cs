using System;
using System.Collections;
using System.Collections.Generic;

using FluentAssertions;

using NewModel.Shared.Utils;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.Utils
{
    [TestFixture]
    public sealed class TypeExtensionsTests
    {
        #region ' Sample classes '
        sealed class CorrectSample : IEnumerable<int>
        {
            public void Add(int item)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<int> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        sealed class CorrectGenericSample<T> : IEnumerable<T>
        {
            public void Add(int item)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<T> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        sealed class SampleWithPrivateAdd<T> : IEnumerable<T>
        {
            void Add(int item)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<T> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        sealed class SampleWithManyArgumentsInAdd<T> : IEnumerable<T>
        {
            void Add(int item, int what)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<T> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        sealed class SampleWithoutAdd<T> : IEnumerable<T>
        {
            public IEnumerator<T> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        #endregion

        [Test]
        public void ItemType_When_IEnumerable_Generic_Descendant_With_Add_Should_Return_Item_Type()
        {
            typeof(CorrectGenericSample<int>).ItemType().Should().Be(typeof(int));
        }        
        [Test]
        public void ItemType_When_IEnumerable_Descendant_With_Add_Should_Return_Item_Type()
        {
            typeof(CorrectSample).ItemType().Should().Be(typeof(int));
        }
        [Test]
        public void ItemType_Without_Add_Method_Should_Return_Null()
        {
            typeof (SampleWithoutAdd<int>).ItemType().Should().BeNull();
        }
        [Test]
        public void ItemType_With_Private_Add_Method_Should_Return_Null()
        {
            typeof (SampleWithPrivateAdd<int>).ItemType().Should().BeNull();
        }
        [Test]
        public void ItemType_With_Many_Arguments_In_Add_Method_Should_Return_Null()
        {
            typeof (SampleWithManyArgumentsInAdd<int>).ItemType().Should().BeNull();
        }
        [Test]
        public void ItemType_When_List_Should_Return_Item_Type()
        {
            typeof(List<int>).ItemType().Should().Be(typeof(int));
        }
        [Test]
        public void ItemType_When_Not_Collection_Should_Return_Null()
        {
            typeof (int).ItemType().Should().BeNull();
        }
    }
}
