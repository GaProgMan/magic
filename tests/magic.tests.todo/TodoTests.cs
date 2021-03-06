/*
 * Magic, Copyright(c) Thomas Hansen 2019 - thomas@gaiasoul.com
 * Licensed as Affero GPL unless an explicitly proprietary license has been obtained.
 */

using Mapster;
using magic.tests.common;
using magic.services.todo;
using magic.web.controller.email;
using magic.tests.common.utilities;
using db = magic.model.todo;
using www = magic.web.model.todo;
using Xunit;

namespace magic.tests.todo
{
    public class TodoTests : CrudTest<TodoController, www.Todo, db.Todo>
    {
        #region [ -- Abstract implementations -- ]

        protected override TodoController CreateController(Connection connection)
        {
            return new TodoController(new Adapter(), new TodoService(connection.Session));
        }

        protected override www.Todo CreateModel(int no)
        {
            return new www.Todo
            {
                Header = "Some header_" + no,
                Description = "Some description_" + no,
                Done = false
            };
        }

        protected override void AssertModel(int no, www.Todo todo)
        {
            Assert.Equal("Some header_" + no, todo.Header);
            Assert.Equal("Some description_" + no, todo.Description);
            Assert.False(todo.Done);
        }

        protected override void ModifyModel(www.Todo todo)
        {
            todo.Description = "Modified description";
            todo.Done = true;
            todo.Header = "Modified header";
        }

        protected override void AssertModelAfterModified(www.Todo todo)
        {
            Assert.Equal("Modified header", todo.Header);
            Assert.Equal("Modified description", todo.Description);
            Assert.True(todo.Done);
        }

        #endregion
    }
}
