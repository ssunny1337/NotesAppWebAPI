using Moq;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Repositories.Interfaces;
using NotesAppWebAPI.Services;
using NotesAppWebAPI.Validators.TagValidators;

namespace NotesAppWebAPI.Tests
{
    public class TagServiceTests
    {        
        private Mock<ITagRepository> _tagRepositoryMock;
        private TagService _tagService;

        [SetUp]
        public void Setup()
        {            
            _tagRepositoryMock = new Mock<ITagRepository>();
            _tagService = new TagService(_tagRepositoryMock.Object);
        }

        #region Service tests

        [Test]
        public async Task GetNoteByIdAsync_NoteExists_ReturnsNote()
        {
            var tag = new Tag { Id = 1, Name = "Test" };
            _tagRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(tag);

            var result = await _tagService.GetByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(tag));
        }

        [Test]
        public async Task GetTagByIdAsync_TagDoesntExist_ReturnsNull()
        {
            _tagRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Tag)null);

            var result = await _tagService.GetByIdAsync(1);

            Assert.IsNull(result);
        }

        [Test]
        public async Task CreateTagAsync_ValidInputData_ReturnsIdOfCreatedTag()
        {
            var command = new CreateTagCommand
            {
                Name = "string"
            };

            _tagRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Tag>()))
                .Callback<Tag>(tag => tag.Id = 1)
                .Returns(Task.CompletedTask);

            var result = await _tagService.AddAsync(command.Name);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateTagAsync_ValidInputDataAndTagExists_ReturnsTrue()
        {
            var command = new UpdateTagCommand
            {
                TagId = 1,
                Name = "string"
            };

            var tag = new Tag { Id = 1, Name = "string" };

            _tagRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(tag);
            _tagRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Tag>()))
                .Returns(Task.CompletedTask);

            var result = await _tagService.UpdateAsync(command.TagId, command.Name);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateTagAsync_ValidInputDataAndTagDoesntExist_ReturnsFalse()
        {
            var command = new UpdateTagCommand
            {
                TagId = 1,
                Name = "string"
            };

            _tagRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Tag>()))
                .Returns(Task.CompletedTask);

            var result = await _tagService.UpdateAsync(command.TagId, command.Name);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteTagAsync_TagExists_ReturnsTrue()
        {
            var tag = new Tag { Id = 1, Name = "string" };

            _tagRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Tag>()))
                .Returns(Task.CompletedTask);
            _tagRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(tag);

            var result = await _tagService.DeleteAsync(tag.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteTagAsync_TagDoesntExist_ReturnsFalse()
        {
            _tagRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Tag>()))
                .Returns(Task.CompletedTask);

            var result = await _tagService.DeleteAsync(1);

            Assert.IsFalse(result);
        }

        #endregion

        #region Validation tests

        [Test]
        public void GetTagByIdAsync_IdIsLessThanZero_ValidationFailed()
        {
            var validator = new GetTagCommandValidator();

            var result = validator.Validate(new GetTagCommand() { TagId = -1 });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void CreateTagAsync_NameIsEmpty_ValidationFailed()
        {
            var validator = new CreateTagCommandValidator();

            var result = validator.Validate(new CreateTagCommand() { Name = string.Empty });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void UpdateTagAsync_IdIsLessThanZero_ValidationFailed()
        {
            var validator = new UpdateTagCommandValidator();

            var result = validator.Validate(new UpdateTagCommand() { TagId = -1, Name = "string" });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void UpdateTagAsync_NameIsEmpty_ValidationFailed()
        {
            var validator = new UpdateTagCommandValidator();

            var result = validator.Validate(new UpdateTagCommand() { TagId = 1, Name = string.Empty });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void DeleteTagAsync_IdIsLessThanZero_ValidationFailed()
        {
            var validator = new DeleteTagCommandValidator();

            var result = validator.Validate(new DeleteTagCommand() { TagId = -1 });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        #endregion        
    }
}