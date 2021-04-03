namespace Fifth.Test.CodeGeneration
{
    using Fifth.CodeGeneration;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture, Category("IL"), Category("Code Generation")]
    public class TemplateLoaderTests
    {
        [Test]
        public void CanLoadDirTemplates()
        {
            var sut = TemplateLoader.LoadTemplates();
            var t = sut.GetInstanceOf("namespace");
            sut.Should().NotBeNull();
            t.Should().NotBeNull();
            t.Add("ns", "hello world");
            var s = t.Render();
            s.Should().NotBeNullOrWhiteSpace();
        }
        [Test]
        public void CanLoadEmbeddedResources()
        {
            var sut = TemplateLoader.LoadEmbeddedTemplates();
            sut.Should().NotBeNullOrEmpty();
        }
    }
}
