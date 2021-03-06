﻿using CSharpEssentials.ConvertToInterpolatedString;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeRefactorings;
using NUnit.Framework;
using RoslynNUnitLight;

namespace CSharpEssentials.Tests.ConvertToInterpolatedString
{
    public class ConvertToInterpolatedStringRefactoringTests : CodeRefactoringTestFixture
    {
        protected override string LanguageName => LanguageNames.CSharp;
        protected override CodeRefactoringProvider CreateProvider() => new ConvertToInterpolatedStringRefactoring();

        [Test]
        public void SimpleTest()
        {
            const string markupCode = @"
class C
{
    void M()
    {
        var s = [|string.Format(""{0}"", 42)|];
    }
}";

            const string expected = @"
class C
{
    void M()
    {
        var s = $""{42}"";
    }
}";

            TestCodeRefactoring(markupCode, expected);
        }

        [Test]
        public void MultipleSpecifiers()
        {
            const string markupCode = @"
class C
{
    void M()
    {
        var s = [|string.Format(""{0} - {0}"", 42)|];
    }
}";

            const string expected = @"
class C
{
    void M()
    {
        var s = $""{42} - {42}"";
    }
}";

            TestCodeRefactoring(markupCode, expected);
        }

        [Test]
        public void MultipleArguments()
        {
            const string markupCode = @"
class C
{
    void M()
    {
        var s = [|string.Format(""{0} + {1}"", 19, 23)|];
    }
}";

            const string expected = @"
class C
{
    void M()
    {
        var s = $""{19} + {23}"";
    }
}";

            TestCodeRefactoring(markupCode, expected);
        }

        [Test]
        public void FormatClause()
        {
            const string markupCode = @"
class C
{
    void M()
    {
        var s = [|string.Format(""{0:x}"", 42)|];
    }
}";

            const string expected = @"
class C
{
    void M()
    {
        var s = $""{42:x}"";
    }
}";

            TestCodeRefactoring(markupCode, expected);
        }

        [Test]
        public void ConditionalExpression()
        {
            const string markupCode = @"
class C
{
    void M()
    {
        var s = [|string.Format(""{0}"", 42 == 42 ? true : false)|];
    }
}";

            const string expected = @"
class C
{
    void M()
    {
        var s = $""{(42 == 42 ? true : false)}"";
    }
}";

            TestCodeRefactoring(markupCode, expected);
        }

        [Test]
        public void ConditionalExpressionAndFormatClause()
        {
            const string markupCode = @"
class C
{
    void M()
    {
        var s = [|string.Format(""{0:x}"", 42 == 42 ? true : false)|];
    }
}";

            const string expected = @"
class C
{
    void M()
    {
        var s = $""{(42 == 42 ? true : false):x}"";
    }
}";

            TestCodeRefactoring(markupCode, expected);
        }
    }
}
