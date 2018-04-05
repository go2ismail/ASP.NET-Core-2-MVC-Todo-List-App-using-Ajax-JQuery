$(document).ready(function () {

    window.EditTodo = EditTodo;

    RenderGrid();



    function ResetForm() {
        $('#formTodo').each(function () {
            this.reset();
        });
    }

    function EditTodo(id) {

        if (id != null && id > 0) {

            $.ajax({
                type: "GET",
                url: "/api/Todo/GetTodoById",
                data: { id: id },
                success: function (result) {

                    $('#todoId').val(result.todoId);
                    $('#title').val(result.title);
                    $('#description').val(result.description);
                    

                    $('#modalForm').modal('toggle');
                }
            });
        }
    }

    function RenderGrid() {
        $.ajax({
            type: "GET",
            url: "/api/Todo/GetTodo",
            success: function (result) {
                if (result.length == 0) {
                    $('table').addClass('hidden');
                } else {
                    $('table').removeClass('hidden');
                    $('#tbody').children().remove();

                    $(result).each(function (i) {
                        var tbody = $('#tbody');
                        var tr = "<tr>";
                        tr += "<td>" + result[i].title;
                        tr += "<td>" + result[i].description;
                        tr += "<td>" + "<button class='btn btn-info btn-xs' onclick=EditTodo(" + result[i].todoId + ")>" + "Edit";
                        tr += "<td>" + "<button class='btn btn-danger btn-xs' onclick=DeleteTodo(" + result[i].todoId + ")>" + "Delete";
                        tbody.append(tr);
                    });
                }
            }
        });
    }

    $('#closeTodo').click(function () {
        ResetForm();
        $('#modalForm').modal('toggle');
    });

    $('#saveTodo').click(function () {
        var todo = $('#formTodo').serialize();
        console.log(todo);
        $.ajax({
            type: "POST",
            url: "/api/Todo/PostTodo",
            data: todo,
            success: function () {

                RenderGrid();
                ResetForm();
                $('#todoId').val(0);
                $('#modalForm').modal('toggle');

            },
            error: function () {

            }
        });

    });

});



