using Moq;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Repositories.Interfaces;
using NotesAppWebAPI.Services;
using NotesAppWebAPI.Validators.NoteValidators;

namespace NotesAppWebAPI.Tests
{
    public class NoteServiceTests
    {
        private Mock<INoteRepository> _noteRepositoryMock;
        private Mock<ITagRepository> _tagRepositoryMock;    
        private NoteService _noteService;

        [SetUp]
        public void Setup()
        {
            _noteRepositoryMock = new Mock<INoteRepository>();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _noteService = new NoteService(_noteRepositoryMock.Object, _tagRepositoryMock.Object);
        }

        #region Service tests

        [Test]
        public async Task GetNoteByIdAsync_NoteExists_ReturnsNote()
        {
            var note = new Note { Id = 1, Title = "Test", Text = "Text" };
            _noteRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(note);

            var result = await _noteService.GetByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(note));
        }

        [Test]
        public async Task GetNoteByIdAsync_NoteDoesntExist_ReturnsNull()
        {
            _noteRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Note)null);

            var result = await _noteService.GetByIdAsync(1);

            Assert.IsNull(result);
        }

        [Test]
        public async Task CreateNoteAsync_ValidInputData_ReturnsIdOfCreatedNote()
        {
            var command = new CreateNoteCommand 
            {
                Title = "string",
                Text = "string",
                TagsIds = new List<int> { 1 }
            };

            _noteRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Note>()))
                .Callback<Note>(note => note.Id = 1)
                .Returns(Task.CompletedTask);
            _tagRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Tag> { new Tag { Id = 1, Name = "name" } });            

            var result = await _noteService.AddAsync(command.Title, command.Text, command.TagsIds);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateNoteAsync_ValidInputDataAndNoteExists_ReturnsTrue()
        {
            var command = new UpdateNoteCommand
            {
                NoteId = 1,
                Title = "string",
                Text = "string",
                TagsIds = new List<int> { 1 }
            };

            var note = new Note { Id = 1, Title = "string", Text = "string" };

            _noteRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(note);
            _noteRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Note>()))                
                .Returns(Task.CompletedTask);
            _tagRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Tag> { new Tag { Id = 1, Name = "name" } });

            var result = await _noteService.UpdateAsync(command.NoteId, command.Title, command.Text, command.TagsIds);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateNoteAsync_ValidInputDataAndNoteDoesntExist_ReturnsFalse()
        {
            var command = new UpdateNoteCommand
            {
                NoteId = 1,
                Title = "string",
                Text = "string",
                TagsIds = new List<int> { 1 }
            };

            _noteRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Note>()))
                .Returns(Task.CompletedTask);
            _tagRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Tag> { new Tag { Id = 1, Name = "name" } });

            var result = await _noteService.UpdateAsync(command.NoteId, command.Title, command.Text, command.TagsIds);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteNoteAsync_NoteExists_ReturnsTrue()
        {
            var note = new Note { Id = 1, Title = "string", Text = "string" };

            _noteRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Note>()))
                .Returns(Task.CompletedTask);
            _noteRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(note);

            var result = await _noteService.DeleteAsync(note.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteNoteAsync_NoteDoesntExist_ReturnsFalse()
        {
            _noteRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Note>()))
                .Returns(Task.CompletedTask);

            var result = await _noteService.DeleteAsync(1);

            Assert.IsFalse(result);
        }

        #endregion

        #region Validation tests

        [Test]
        public void GetNoteByIdAsync_IdIsLessThanZero_ValidationFailed()
        {
            var validator = new GetNoteCommandValidator();

            var result = validator.Validate(new Commands.GetNoteCommand() { NoteId = -1 });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void CreateNoteAsync_TitleIsEmpty_ValidationFailed()
        {
            var validator = new CreateNoteCommandValidator();

            var result = validator.Validate(new Commands.CreateNoteCommand() { Title = string.Empty, Text = "string", TagsIds = new List<int> { 1, 2 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void CreateNoteAsync_TextIsEmpty_ValidationFailed()
        {
            var validator = new CreateNoteCommandValidator();

            var result = validator.Validate(new Commands.CreateNoteCommand() { Title = "string", Text = string.Empty, TagsIds = new List<int> { 1, 2 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void CreateNoteAsync_TagIdIsLessThanZero_ValidationFailed()
        {
            var validator = new CreateNoteCommandValidator();

            var result = validator.Validate(new Commands.CreateNoteCommand() { Title = "string", Text = "string", TagsIds = new List<int> { -1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void UpdateNoteAsync_IdIsLessThanZero_ValidationFailed()
        {
            var validator = new UpdateNoteCommandValidator();

            var result = validator.Validate(new Commands.UpdateNoteCommand() { NoteId = -1, Title = "string", Text = "string", TagsIds = new List<int> { 1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void UpdateNoteAsync_TitleIsEmpty_ValidationFailed()
        {
            var validator = new UpdateNoteCommandValidator();

            var result = validator.Validate(new Commands.UpdateNoteCommand() { NoteId = 1, Title = string.Empty, Text = "string", TagsIds = new List<int> { 1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void UpdateNoteAsync_TextIsEmpty_ValidationFailed()
        {
            var validator = new UpdateNoteCommandValidator();

            var result = validator.Validate(new Commands.UpdateNoteCommand() { NoteId = 1, Title = "string", Text = string.Empty, TagsIds = new List<int> { 1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void UpdateNoteAsync_TagIdIsLessThanZero_ValidationFailed()
        {
            var validator = new UpdateNoteCommandValidator();

            var result = validator.Validate(new Commands.UpdateNoteCommand() { NoteId = 1, Title = "string", Text = "string", TagsIds = new List<int> { -1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void DeleteNoteAsync_IdIsLessThanZero_ValidationFailed()
        {
            var validator = new DeleteNoteCommandValidator();

            var result = validator.Validate(new Commands.DeleteNoteCommand() { NoteId = -1 });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        #endregion        
    }
}