function SetNameOfSort(Element) {
    var text = document.getElementById("nameOfSort");
    if (Element.value == "byName") {
        text.innerHTML = "Сортировать: By Name";
    } else {
        text.innerHTML = "Сортировать: By Time";
    }
    return false;
}

function setChangeToController(Element) {
    if (Element.value == "") {
        taskName = "";
        $.ajax({
            type: 'POST',
            url: window.location.pathname + 'taskList/FilterByTaskName',
            data: taskName,
            success: function (data) {
                $('#table').html(data);
            },
            error: function (xhr, str) {
                alert('Возникла ошибка: ' + xhr.responseCode);
            }
        });
    }
}