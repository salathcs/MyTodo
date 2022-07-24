using AutoMapper;
using DataTransfer.DataTransferObjects;
using Entities.Base;
using Entities.Models;
using MyAuth_lib.Interfaces;
using MyLogger.Interfaces;
using MyTodo_Todos.Interfaces;
using MyTodo_Todos.Services;

namespace UnitTests.MyTodo_Todos
{
    public class CrudService_Tests
    {
        private readonly Mock<IMyLogger> logger;
        private readonly Mock<ICrudRepository> crudRepository;
        private readonly Mock<IUserIdentityHelper> userIdentityHelper;
        private readonly Mock<IMapper> mapper;

        private readonly ICrudService crudService;

        public CrudService_Tests()
        {
            //arrange before every test
            logger = new Mock<IMyLogger>();
            crudRepository = new Mock<ICrudRepository>();
            userIdentityHelper = new Mock<IUserIdentityHelper>();
            mapper = new Mock<IMapper>();

            crudService = new CrudService(logger.Object, crudRepository.Object, userIdentityHelper.Object, mapper.Object);
        }

        #region GetAll
        [Fact]
        public void GetAll_RepoCalled()
        {
            //arrange

            //act
            crudService.GetAll();

            //assert
            crudRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAll_ReturnValueCount()
        {
            //arrange
            var arrangedResult = new[] { new TodoDto(), new TodoDto() };
            crudRepository.Setup(x => x.GetAll()).Returns(arrangedResult);

            //act
            var result = crudService.GetAll();

            //assert
            Assert.Equal(arrangedResult.Count(), result.Count());
        }

        [Fact]
        public void GetAll_ReturnValueUnmodified()
        {
            //arrange
            var arrangedResult = new[] { new TodoDto { Id = 1001, Description = "desc", Expiration = DateTime.UtcNow, Title = "title", UserId = 101 } };
            crudRepository.Setup(x => x.GetAll()).Returns(arrangedResult);

            //act
            var result = crudService.GetAll();

            //assert
            Assert.Equal(arrangedResult.First().Id, result.First().Id);
            Assert.Equal(arrangedResult.First().Description, result.First().Description);
            Assert.Equal(arrangedResult.First().Expiration, result.First().Expiration);
            Assert.Equal(arrangedResult.First().Title, result.First().Title);
            Assert.Equal(arrangedResult.First().UserId, result.First().UserId);
        }
        #endregion GetAll

        #region GetById
        [Fact]
        public void GetById_RepoCalled()
        {
            //arrange
            long id = 101;

            //act
            crudService.GetById(id);

            //assert
            crudRepository.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public void GetById_ReturnValueNull()
        {
            //arrange
            long id = 101;
            crudRepository.Setup(x => x.GetById(id)).Returns<TodoDto>(null);

            //act
            var result = crudService.GetById(id);

            //assert
            Assert.Null(result);
        }

        [Fact]
        public void GetById_ReturnValueUnmodified()
        {
            //arrange
            long id = 101;
            var arrangedResult = new TodoDto { Id = 1001, Description = "desc", Expiration = DateTime.UtcNow, Title = "title", UserId = 101 };
            crudRepository.Setup(x => x.GetById(id)).Returns(arrangedResult);

            //act
            var result = crudService.GetById(id);

            //assert
            Assert.NotNull(result);
            Assert.Equal(arrangedResult.Id, result.Id);
            Assert.Equal(arrangedResult.Description, result.Description);
            Assert.Equal(arrangedResult.Expiration, result.Expiration);
            Assert.Equal(arrangedResult.Title, result.Title);
            Assert.Equal(arrangedResult.UserId, result.UserId);
        }
        #endregion GetById

        #region Create
        [Fact]
        public void Create_RepoCalled()
        {
            //arrange
            var param = new TodoDto();
            var mappedTodo = new Todo();
            mapper.Setup(x => x.Map<Todo>(param)).Returns(mappedTodo);

            //act
            crudService.Create(param);

            //assert
            crudRepository.Verify(x => x.Create(mappedTodo), Times.Once);
        }
        [Fact]
        public void Create_InfoLogCalled()
        {
            //arrange
            var param = new TodoDto();
            var mappedTodo = new Todo();
            mapper.Setup(x => x.Map<Todo>(param)).Returns(mappedTodo);

            //act
            crudService.Create(param);

            //assert
            logger.Verify(x => x.Info(It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.AtLeastOnce());
        }

        [Fact]
        public void Create_IdentityHelperCalled()
        {
            //arrange
            var param = new TodoDto();
            var mappedTodo = new Todo { Id = 1 };
            mapper.Setup(x => x.Map<Todo>(param)).Returns(mappedTodo);

            //act
            crudService.Create(param);

            //assert
            userIdentityHelper.Verify(x => x.TryFillExtendedEntityFields(It.IsAny<BaseEntity>()), Times.AtLeastOnce());
        }

        [Fact]
        public void Create_UserId()
        {
            //arrange
            var param = new TodoDto();
            var mappedTodo = new Todo { Id = 1 };
            long userId = 101;
            mapper.Setup(x => x.Map<Todo>(param)).Returns(mappedTodo);
            userIdentityHelper.Setup(x => x.GetUserId()).Returns(userId);

            //act
            crudService.Create(param);

            //assert
            crudRepository.Verify(x => x.Create(It.Is<Todo>(x => x.UserId == userId)));
        }

        [Fact]
        public void Create_ReturnValue()
        {
            //arrange
            var param = new TodoDto();
            var mappedTodo = new Todo { Id = 1 };
            mapper.Setup(x => x.Map<Todo>(param)).Returns(mappedTodo);

            //act
            crudService.Create(param);

            //assert
            Assert.Equal(mappedTodo.Id, param.Id);
        }
        #endregion Create

        #region Update
        [Fact]
        public void Update_RepoCalled()
        {
            //arrange
            var param = new TodoDto();
            var entityTodo = new Todo();
            crudRepository.Setup(x => x.GetEntityById(It.IsAny<long>())).Returns(entityTodo);

            //act
            crudService.Update(param);

            //assert
            crudRepository.Verify(x => x.Update(It.IsAny<Todo>()), Times.Once);
        }
        [Fact]
        public void Update_InfoLogCalled()
        {
            //arrange
            var param = new TodoDto();
            var entityTodo = new Todo();
            crudRepository.Setup(x => x.GetEntityById(It.IsAny<long>())).Returns(entityTodo);

            //act
            crudService.Update(param);

            //assert
            logger.Verify(x => x.Info(It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.AtLeastOnce());
        }

        [Fact]
        public void Update_IdentityHelperCalled()
        {
            //arrange
            var param = new TodoDto();
            var entityTodo = new Todo();
            crudRepository.Setup(x => x.GetEntityById(It.IsAny<long>())).Returns(entityTodo);

            //act
            crudService.Update(param);

            //assert
            userIdentityHelper.Verify(x => x.TryFillExtendedEntityFields(It.IsAny<BaseEntity>()), Times.AtLeastOnce());
        }

        [Fact]
        public void Update_TodoId()
        {
            //arrange
            long todoId = 101;
            var param = new TodoDto { Id = todoId };
            var entityTodo = new Todo();
            crudRepository.Setup(x => x.GetEntityById(todoId)).Returns(entityTodo);

            //act
            crudService.Update(param);

            //assert
            crudRepository.VerifyAll();
        }

        [Fact]
        public void Update_ReturnFalse()
        {
            //arrange
            var param = new TodoDto();
            crudRepository.Setup(x => x.GetEntityById(It.IsAny<long>())).Returns<Todo>(null);

            //act
            var result = crudService.Update(param);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Update_ReturnValue()
        {
            //arrange
            var param = new TodoDto();
            var entityTodo = new Todo();
            crudRepository.Setup(x => x.GetEntityById(It.IsAny<long>())).Returns(entityTodo);

            //act
            var result = crudService.Update(param);

            //assert
            Assert.True(result);
        }
        #endregion Update

        #region Delete
        [Fact]
        public void Delete_RepoCalled()
        {
            //arrange
            var todoId = 101;

            //act
            crudService.Delete(todoId);

            //assert
            crudRepository.Verify(x => x.Delete(It.IsAny<long>()), Times.Once);
        }
        [Fact]
        public void Delete_InfoLogCalled()
        {
            //arrange
            var todoId = 101;

            //act
            crudService.Delete(todoId);

            //assert
            logger.Verify(x => x.Info(It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.AtLeastOnce());
        }

        [Fact]
        public void Delete_TodoId()
        {
            //arrange
            var todoId = 101;

            //act
            crudService.Delete(todoId);

            //assert
            crudRepository.Verify(x => x.Delete(todoId));
        }

        [Fact]
        public void Delete_ReturnFalse()
        {
            //arrange
            var todoId = 101;
            crudRepository.Setup(x => x.Delete(It.IsAny<long>())).Returns<Todo>(null);

            //act
            var result = crudService.Delete(todoId);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Delete_ReturnTrue()
        {
            //arrange
            var todoId = 101;
            crudRepository.Setup(x => x.Delete(It.IsAny<long>())).Returns(new Todo());

            //act
            var result = crudService.Delete(todoId);

            //assert
            Assert.True(result);
        }
        #endregion Delete
    }
}