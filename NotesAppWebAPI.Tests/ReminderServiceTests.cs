using Moq;
using NotesAppWebAPI.Commands;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Repositories.Interfaces;
using NotesAppWebAPI.Services;
using NotesAppWebAPI.Validators.ReminderValidators;

namespace NotesAppWebAPI.Tests
{
    public class ReminderServiceTests
    {
        private Mock<IReminderRepository> _reminderRepositoryMock;
        private Mock<ITagRepository> _tagRepositoryMock;
        private ReminderService _reminderService;

        [SetUp]
        public void Setup()
        {
            _reminderRepositoryMock = new Mock<IReminderRepository>();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _reminderService = new ReminderService(_reminderRepositoryMock.Object, _tagRepositoryMock.Object);
        }

        #region Service tests

        [Test]
        public async Task GetReminderByIdAsync_ReminderExists_ReturnsReminder()
        {
            var reminder = new Reminder { Id = 1, Title = "Test", Text = "Text", ReminderTime = new DateTime(2026, 1, 1) };
            _reminderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(reminder);

            var result = await _reminderService.GetByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(reminder));
        }

        [Test]
        public async Task GetReminderByIdAsync_ReminderDoesntExist_ReturnsNull()
        {
            _reminderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Reminder)null);

            var result = await _reminderService.GetByIdAsync(1);

            Assert.IsNull(result);
        }

        [Test]
        public async Task CreateReminderAsync_ValidInputData_ReturnsIdOfCreatedReminder()
        {
            var command = new CreateReminderCommand
            {                
                Title = "string",
                Text = "string",
                ReminderTime = new DateTime(2026, 1, 1),
                TagsIds = new List<int> { 1 }
            };

            _reminderRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Reminder>()))
                .Callback<Reminder>(reminder => reminder.Id = 1)
                .Returns(Task.CompletedTask);
            _tagRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Tag> { new Tag { Id = 1, Name = "name" } });

            var result = await _reminderService.AddAsync(command.Title, command.Text, command.ReminderTime, command.TagsIds);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateReminderAsync_ValidInputDataAndReminderExists_ReturnsTrue()
        {
            var command = new UpdateReminderCommand
            {
                ReminderId = 1,
                Title = "string",
                Text = "string",
                ReminderTime = new DateTime(2026, 1, 1),
                TagsIds = new List<int> { 1 }
            };

            var reminder = new Reminder { Id = 1, Title = "string", Text = "string", ReminderTime = new DateTime(2026, 1, 1) };

            _reminderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(reminder);
            _reminderRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Reminder>()))
                .Returns(Task.CompletedTask);
            _tagRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Tag> { new Tag { Id = 1, Name = "name" } });

            var result = await _reminderService.UpdateAsync(command.ReminderId, command.Title, command.Text, command.ReminderTime, command.TagsIds);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateReminderAsync_ValidInputDataAndReminderDoesntExist_ReturnsFalse()
        {
            var command = new UpdateReminderCommand
            {
                ReminderId = 1,
                Title = "string",
                Text = "string",
                ReminderTime = new DateTime(2026, 1, 1),
                TagsIds = new List<int> { 1 }
            };

            _reminderRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Reminder>()))
                .Returns(Task.CompletedTask);
            _tagRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Tag> { new Tag { Id = 1, Name = "name" } });

            var result = await _reminderService.UpdateAsync(command.ReminderId, command.Title, command.Text, command.ReminderTime, command.TagsIds);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteReminderAsync_ReminderExists_ReturnsTrue()
        {
            var reminder = new Reminder { Id = 1, Title = "string", Text = "string", ReminderTime = new DateTime(2026, 1, 1) };

            _reminderRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Reminder>()))
                .Returns(Task.CompletedTask);
            _reminderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(reminder);

            var result = await _reminderService.DeleteAsync(1);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteReminderAsync_ReminderDoesntExist_ReturnsFalse()
        {
            _reminderRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<Reminder>()))
                .Returns(Task.CompletedTask);

            var result = await _reminderService.DeleteAsync(1);

            Assert.IsFalse(result);
        }

        #endregion

        #region Validation tests

        [Test]
        public void GetReminderByIdAsync_IdIsLessThanZero_ValidationFailed()
        {
            var validator = new GetReminderCommandValidator();

            var result = validator.Validate(new GetReminderCommand() { ReminderId = -1 });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void CreateReminderAsync_TitleIsEmpty_ValidationFailed()
        {
            var validator = new CreateReminderCommandValidator();

            var result = validator.Validate(new CreateReminderCommand()
                { Title = string.Empty, Text = "string", ReminderTime = new DateTime(2026, 1, 1), TagsIds = new List<int> { 1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void CreateReminderAsync_TextIsEmpty_ValidationFailed()
        {
            var validator = new CreateReminderCommandValidator();

            var result = validator.Validate(new CreateReminderCommand()
                { Title = "string", Text = string.Empty, ReminderTime = new DateTime(2026, 1, 1), TagsIds = new List<int> { 1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void CreateReminderAsync_ReminderTimeIsNotInTheFuture_ValidationFailed()
        {
            var validator = new CreateReminderCommandValidator();

            var result = validator.Validate(new CreateReminderCommand()
            { Title = "string", Text = "string", ReminderTime = new DateTime(2025, 1, 1), TagsIds = new List<int> { 1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void CreateReminderAsync_TagIdIsLessThanZero_ValidationFailed()
        {
            var validator = new CreateReminderCommandValidator();

            var result = validator.Validate(new CreateReminderCommand() 
                { Title = "string", Text = "string", ReminderTime = new DateTime(2026, 1, 1), TagsIds = new List<int> { -1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void UpdateReminderAsync_IdIsLessThanZero_ValidationFailed()
        {
            var validator = new UpdateReminderCommandValidator();

            var result = validator.Validate(new UpdateReminderCommand() 
                { ReminderId = -1, Title = "string", Text = "string", ReminderTime = new DateTime(2026, 1, 1), TagsIds = new List<int> { 1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void UpdateReminderAsync_TitleIsEmpty_ValidationFailed()
        {
            var validator = new UpdateReminderCommandValidator();

            var result = validator.Validate(new UpdateReminderCommand() 
                { ReminderId = 1, Title = string.Empty, Text = "string", ReminderTime = new DateTime(2026, 1, 1), TagsIds = new List<int> { 1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void UpdateReminderAsync_TextIsEmpty_ValidationFailed()
        {
            var validator = new UpdateReminderCommandValidator();

            var result = validator.Validate(new UpdateReminderCommand() 
                { ReminderId = 1, Title = "string", Text = string.Empty, ReminderTime = new DateTime(2026, 1, 1), TagsIds = new List<int> { 1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void UpdateReminderAsync_ReminderTimeIsNotInTheFuture_ValidationFailed()
        {
            var validator = new UpdateReminderCommandValidator();

            var result = validator.Validate(new UpdateReminderCommand() 
                { ReminderId = 1, Title = "string", Text = "string", ReminderTime = new DateTime(2025, 1, 1), TagsIds = new List<int> { 1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void UpdateReminderAsync_TagIdIsLessThanZero_ValidationFailed()
        {
            var validator = new UpdateReminderCommandValidator();

            var result = validator.Validate(new UpdateReminderCommand() 
                { ReminderId = 1, Title = "string", Text = "string", ReminderTime = new DateTime(2026, 1, 1), TagsIds = new List<int> { -1 } });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        [Test]
        public void DeleteReminderAsync_IdIsLessThanZero_ValidationFailed()
        {
            var validator = new DeleteReminderCommandValidator();

            var result = validator.Validate(new DeleteReminderCommand() { ReminderId = -1 });

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors, Has.Count.GreaterThan(0));
        }

        #endregion        
    }
}